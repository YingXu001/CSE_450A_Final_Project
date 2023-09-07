using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust the speed as needed
    public float rotationSpeed = 2f;  // Adjust the rotation speed as needed
    public Rigidbody2D submarineRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        submarineRigidbody = GetComponent<Rigidbody2D>();
        // Lock the cursor to the game window to prevent it from going off-screen.
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        // W and S to move forward and backward
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector.
        Vector2 moveDirection = transform.up * verticalInput * moveSpeed;

        // Apply movement to the submarine.
        submarineRigidbody.velocity = moveDirection;

        // Handle player input for rotation.
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate rotation.
        float rotation = -horizontalInput * rotationSpeed;

        // Apply rotation to the submarine.
        submarineRigidbody.angularVelocity = rotation;
        


        // Clamp the submarine's velocity to prevent excessive speed.
        if (submarineRigidbody.velocity.magnitude > moveSpeed)
        {
            submarineRigidbody.velocity = submarineRigidbody.velocity.normalized * moveSpeed;
        }
    }
}
