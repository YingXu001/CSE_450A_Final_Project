using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;  // This will be your player
    public float attackRange = 2f;  // The range at which the enemy can attack the player
    public float attackCooldown = 1f;  // Time between attacks
    private float nextAttackTime = 0f;

    void Start()
    {
        // Check the distance between the enemy and the player
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // If the player is within attack range and the cooldown has passed
        if (distanceToTarget <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    void Attack()
    {
        // Attack logic here.
        // E.g., reduce player's health
        Debug.Log("Attacked the player!");
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}