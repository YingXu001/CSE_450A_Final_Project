using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar healthBar;
    private SubController player; // get the submarine object

    void Start()
    {
        curHealth = maxHealth;
        player = FindObjectOfType<SubController>();
    }
    void Update()
    {
        if(player.mechaSprite)
        {
            maxHealth = 200;
        }
    }
    public void DamagePlayer(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
    }

    public void GetHealth(int health)
    {
        curHealth += health;
        healthBar.SetHealth(curHealth);
    }
}