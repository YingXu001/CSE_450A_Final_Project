using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    // Outlets
    public Rigidbody2D submarineRigidbody;
    public Transform submarinePivot;
    public GameObject[] bulletPrefabs;
    private AudioSource engine_sound;

    // States Tracking
    public float moveSpeed = 50f;  // Adjust the speed as needed
    public float rotationSpeed = 30f;  // Adjust the rotation speed as needed
    private int currentBullet = 1;
    private bool isMoving = false;

    //ammo counts
    public int numAmmo = 10;
    //sound effects
    public AudioClip out_of_ammo_sound;
    public AudioClip laser_shot_sound;

    // Start is called before the first frame update
    void Start()
    {
        submarineRigidbody = GetComponent<Rigidbody2D>();
        engine_sound = GetComponent<AudioSource>();
        // Lock the cursor to the game window to prevent it from going off-screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // switch between three levels of speed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveSpeed = 50f;
            rotationSpeed = 30f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            moveSpeed = 100f;
            rotationSpeed = 50f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            moveSpeed = 150f;
            rotationSpeed = 70f;
        }

        // W and S to move forward and backward
        float verticalInput = Input.GetAxis("Vertical");
        submarineRigidbody.AddRelativeForce(Vector2.up * verticalInput * moveSpeed * Time.deltaTime);
        

        // a and d for rotation.
        float horizontalInput = Input.GetAxis("Horizontal");
        submarineRigidbody.AddTorque(-horizontalInput * rotationSpeed * Time.deltaTime);

        // if submarine is moving, play engine sound clip, if not, stop the clip after one second
        if (Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f)
        {
            // The submarine is moving, play the sound
            if (!engine_sound.isPlaying)
            {
                engine_sound.Play();
            }
        }
        else
        {
            // The submarine has stopped moving, stop the sound after a delay
            StartCoroutine(StopSoundAfterDelay(1.0f));
        }

        // p for switching weapons
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchToNextBulletPrefab();
        }

        // space for firing the bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(numAmmo == 0)
            {
                AudioSource.PlayClipAtPoint(out_of_ammo_sound, transform.position);
                return;
            }
            fireBullets();
               
        }
    }

    // function for switching bullets
    private void SwitchToNextBulletPrefab()
    {
        // Increment the currentBulletPrefabIndex to switch to the next bullet prefab
        currentBullet++;

        // If we reach the end of the array, wrap around to the first prefab
        if (currentBullet >= bulletPrefabs.Length)
        {
            currentBullet = 0;
        }
    }

    // function for firing the bullet and playing the sound effect
    public void fireBullets()
    {
        GameObject bulletPrefab = bulletPrefabs[currentBullet];
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = submarinePivot.rotation;
        numAmmo--;
        AudioSource.PlayClipAtPoint(laser_shot_sound, transform.position);
    }

    private System.Collections.IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        engine_sound.Stop();
    }

}
