using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float travelDistance = 3f;
    private bool canMove = true;

    private Vector2 moveDirection;
    private Vector2 originalPosition;
    private bool movingOutwards = true;

    private SubController player; // get the submarine object

    private void Start()
    {
        originalPosition = transform.position;
        PickRandomDirection();

        player = FindObjectOfType<SubController>(); // find the SubController type at the start
    }

    private void Update()
    {
        MoveInDirection();
        AdjustDirectionIfNeeded();
    }

    private void MoveInDirection()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void AdjustDirectionIfNeeded()
    {
        if (IsOutsideBoundaries())
        {
            movingOutwards = false;
            ReverseDirection();
        }
        else if (IsNearStartingPosition())
        {
            movingOutwards = true;
            PickRandomDirection();
        }

        AdjustPositionWithinRadius();
    }

    private bool IsOutsideBoundaries()
    {
        return Vector2.Distance(transform.position, originalPosition) > travelDistance && movingOutwards;
    }

    private bool IsNearStartingPosition()
    {
        return Vector2.Distance(transform.position, originalPosition) < 0.1f && !movingOutwards;
    }

    private void ReverseDirection()
    {
        moveDirection *= -1;
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
            DamagePlayerIfPossible(collision.gameObject);
            player.IncreaseEnergy(30);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            player.IncreaseEnergy(30);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Freeze-Bullet"))
        {
            canMove = false;
            Destroy(collision.gameObject);
            print("freeze!");
        }
    }

    private void DamagePlayerIfPossible(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        SubController playerController = player.GetComponent<SubController>();

        if (playerController.HasSheild())
        {
            playerController.DeactivateShield();
        }
        else if (playerHealth)
        {
            playerHealth.DamagePlayer(10);
        }
    }
}
