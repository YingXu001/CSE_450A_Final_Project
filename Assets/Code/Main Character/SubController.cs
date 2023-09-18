using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    // Outlets
    public Rigidbody2D submarineRigidbody;
    public Transform submarinePivot;
    public GameObject bulletPrefab;

    // States Tracking
    public float moveSpeed = 100f;  // Adjust the speed as needed
    public float rotationSpeed = 50f;  // Adjust the rotation speed as needed

    //ammo counts
    public int numAmmo = 10;
    //sound effects
    public AudioClip out_of_ammo;
    public AudioClip laser_shot;

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
        submarineRigidbody.AddRelativeForce(Vector2.up * verticalInput * moveSpeed * Time.deltaTime);

        // a and d for rotation.
        float horizontalInput = Input.GetAxis("Horizontal");
        submarineRigidbody.AddTorque(-horizontalInput * rotationSpeed * Time.deltaTime);

        // space for firing the bullet
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(numAmmo == 0)
            {
                AudioSource.PlayClipAtPoint(out_of_ammo, transform.position);
                return;
            }
            fireBullets();
               
        }
    }

    public void fireBullets()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = submarinePivot.rotation;
        numAmmo--;
        AudioSource.PlayClipAtPoint(laser_shot, transform.position);
    }
}
