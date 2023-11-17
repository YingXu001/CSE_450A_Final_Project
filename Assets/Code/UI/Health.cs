using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //health bar
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar healthBar;
    public EnergyBar energyBar;
    public AudioClip take_damage;
    private SubController player; // get the submarine object
    public SpriteRenderer spriteRenderer; // for flash screen when take damage

    void Start()
    {
        curHealth = maxHealth;
        player = FindObjectOfType<SubController>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    void Update()
    {
        energyBar.SetEnergy(player.energyCurrent);
    }
    public void DamagePlayer(int damage)
    {
        AudioSource.PlayClipAtPoint(take_damage, transform.position);
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
        // Enable the flash screen for 0.5 seconds
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;

            // Invoke a method to disable the sprite renderer after 0.5 seconds
            Invoke("DisableSpriteRenderer", 0.5f);
        }
        
    }

    public void GetHealth(int health)
    {
        curHealth += health;
        healthBar.SetHealth(curHealth);
    }

    private void DisableSpriteRenderer()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

    }
}