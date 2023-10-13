using UnityEngine;
using System.Collections;

public class MonsterEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;
    private int timesHit = 0;
    public float moveSpeed = 2f;
    public Vector2 moveDirection;
    public float travelDistance = 3f;

    private Vector2 startingPosition;
    private Vector2 originalPosition;
    private Vector2 previousVelocity;
    private bool movingOutwards = true;
    private float timeSinceLastDirectionChange;
    private float changeDirectionDelay = 0.5f;

    public GameObject monsterBulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;

    private float lastShotTime;

    private SubController player; // get the submarine object

    private void Start()
    {
        originalPosition = transform.position;
        startingPosition = transform.position;
        PickRandomDirection();

        lastShotTime = Time.time;

        player = FindObjectOfType<SubController>(); // find the SubController type at the start
    }

    private void Update()
    {
        Move();
        CheckBoundaries();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Time.time - lastShotTime >= shootInterval)
        {
            ShootBullet();
            lastShotTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(monsterBulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();

        if (bulletRb)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            bulletRb.velocity = randomDirection * bulletSpeed;
        }
    }


    private void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void CheckBoundaries()
    {
        if (Vector2.Distance(transform.position, startingPosition) > travelDistance && movingOutwards)
        {
            movingOutwards = false;
            moveDirection *= -1;
        }
        else if (Vector2.Distance(transform.position, startingPosition) < 0.1f && !movingOutwards)
        {
            movingOutwards = true;
            PickRandomDirection();
        }
        AdjustPositionWithinRadius();
    }

    private void AdjustPositionWithinRadius()
    {
        if (Vector2.Distance(transform.position, originalPosition) > travelDistance)
        {
            Vector2 directionToCenter = (originalPosition - (Vector2)transform.position).normalized;
            transform.position = originalPosition - directionToCenter * travelDistance;
        }
    }

    private void PickRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        previousVelocity = GetComponent<Rigidbody2D>().velocity;

        if (Time.time - timeSinceLastDirectionChange < changeDirectionDelay)
            return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            timesHit++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            HandleCollisionWithPlayer(collision.gameObject);
            StartCoroutine(ResetVelocityAfterDelay(0.1f));
        }

        CheckHitsAndDestroy();
    }

    private void HandleCollisionWithPlayer(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        SubController playerController = player.GetComponent<SubController>();

        if (playerController.HasSheild())
        {
            playerController.DeactivateShield();
        }
        else
        {
            playerHealth?.DamagePlayer(20);
        }

        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            Vector2 pushDirection = -playerRigidbody.velocity.normalized;
            playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }

        timesHit++;
    }

    private void CheckHitsAndDestroy()
    {
        if (timesHit >= 3)
        {
            Destroy(gameObject);
            player.IncreaseEnergy(10);
        }
    }

    private IEnumerator ResetVelocityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody2D>().velocity = previousVelocity;
    }
}
