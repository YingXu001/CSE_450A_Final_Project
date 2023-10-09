
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    // Outlets
    public Rigidbody2D submarineRigidbody;
    public Transform submarinePivot;
    public Transform aimPivot;
    public GameObject[] bulletPrefabs;
    private AudioSource engine_sound;
    GameObject shield;
    public bool isPaused;
    public static SubController instance;

    // States Tracking
    public float moveSpeed = 50f;  // Adjust the speed as needed
    public float rotationSpeed = 30f;  // Adjust the rotation speed as needed
    private int currentBullet = 1;

    //ammo counts
    public int numAmmo = 10;
    //sound effects
    public AudioClip out_of_ammo_sound;
    public AudioClip laser_shot_sound;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor to the game window to prevent it from going off-screen.
        // Cursor.lockState = CursorLockMode.Locked;
        submarineRigidbody = GetComponent<Rigidbody2D>();
        engine_sound = GetComponent<AudioSource>();
        shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            return;
        }
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
            // StartCoroutine(StopSoundAfterDelay(5.0f));
        }

        // p for switching weapons
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchToNextBulletPrefab();
        }

        // Esc for pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InGameMenuController.instance.Show();
        }

        // Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // space for firing the bullet
        if (Input.GetMouseButtonDown(0))
        {
            if (numAmmo == 0)
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
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position;
        Vector3 newRotation = aimPivot.rotation.eulerAngles + new Vector3(0f, 0f, -90f);
        newBullet.transform.rotation = Quaternion.Euler(newRotation);
        numAmmo--;
        AudioSource.PlayClipAtPoint(laser_shot_sound, transform.position);
    }

    private System.Collections.IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        engine_sound.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetAmmo getAmmo = collision.GetComponent<GetAmmo>();
        GetHealth getHealth = collision.GetComponent<GetHealth>();
        GetShield getShield = collision.GetComponent<GetShield>();
        Health playerHealth = gameObject.GetComponent<Health>();
        if (getAmmo)
        {
            numAmmo++;
            Destroy(getAmmo.gameObject);
        }
        else if (getHealth)
        {
            if (playerHealth.curHealth < playerHealth.maxHealth)
            {
                playerHealth.GetHealth(15);
            }
            Destroy(getHealth.gameObject); ;
        } else if (getShield)
        {
            if (!HasSheild())
            {
                ActivateShield();
                Destroy(getShield.gameObject);
            }
        }
    }

    // activate, deactive the shield
    public void ActivateShield()
    {
        shield.SetActive(true);
    }

    public void DeactivateShield()
    {
        shield.SetActive(false);
    }

    // get the status of the shield
    public bool HasSheild()
    {
        return shield.activeSelf;
    }
}
