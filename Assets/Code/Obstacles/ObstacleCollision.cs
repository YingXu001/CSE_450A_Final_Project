using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Stop the player from moving if moving in direction of the wall
        if (collision.gameObject.GetComponent<SubController>())
        {
            Rigidbody2D submarineRigidBody = GetComponent<Rigidbody2D>();

            submarineRigidBody.velocity = Vector3.zero;

        }
    }
}
