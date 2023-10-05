using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector2 moveDirection;
    public float travelDistance = 3f;

    private Vector2 startingPosition;
    private Vector2 originalPosition;
    private bool movingOutwards = true;
    private float timeSinceLastDirectionChange;
    private float changeDirectionDelay = 0.5f;

    private void Start()
    {
        originalPosition = transform.position;
        startingPosition = transform.position;
        PickRandomDirection();
    }

    private void Update()
    {
        Move();
        CheckBoundaries();
    }

    void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void CheckBoundaries()
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            SubController player = collision.gameObject.GetComponent<SubController>();
            if (player.HasSheild())
            {
                player.DeactivateShield();
            }
            else
            {
                if (playerHealth != null)
                {
                    playerHealth.DamagePlayer(10);
                }
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
