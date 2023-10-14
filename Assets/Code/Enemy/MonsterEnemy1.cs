using UnityEngine;
using System.Collections;

public class MonsterEnemy1 : MonoBehaviour
{
    public float pushBackForce = 10f;
    private int timesHit = 0;
    public float moveSpeed = 2f;
    public float travelDistance = 3f;

    private Vector2 startingPosition;
    private bool movingRight = true;

    public GameObject monsterBulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;

    private float lastShotTime;

    private SubController player; // get the submarine object

    private void Start()
    {
        startingPosition = transform.position;

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
        // Debug.Log("Checking if should shoot...");
        if (Time.time - lastShotTime >= shootInterval)
        {
            ShootBullet();
            lastShotTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        // Debug.Log("Shooting bullet!");
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
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void CheckBoundaries()
    {
        if (movingRight && transform.position.x > startingPosition.x + travelDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x < startingPosition.x - travelDistance)
        {
            movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            timesHit++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            HandleCollisionWithPlayer(collision.gameObject);
        }

        CheckHitsAndDestroy();
    }

    private void HandleCollisionWithPlayer(GameObject playerObj)
    {
        Health playerHealth = playerObj.GetComponent<Health>();
        SubController playerController = playerObj.GetComponent<SubController>();

        if (playerController.HasSheild())
        {
            playerController.DeactivateShield();
        }
        else
        {
            playerHealth?.DamagePlayer(20);
        }

        Rigidbody2D playerRigidbody = playerObj.GetComponent<Rigidbody2D>();
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
}
