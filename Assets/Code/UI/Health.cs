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

    private SubController player; // get the submarine object

    void Start()
    {
        curHealth = maxHealth;
        player = FindObjectOfType<SubController>();
    }
    void Update()
    {
        if(player.mechaMode == true)
        {
            maxHealth = 200;
            curHealth = maxHealth;
            healthBar.SetHealth(curHealth);
        }
        energyBar.SetEnergy(player.energyCurrent);
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