using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossHealthBar : MonoBehaviour
{
    private Slider healthBar;
    public GameObject bossObj;
    private FinalBossEnemy boss;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        boss = bossObj.GetComponent<FinalBossEnemy>();
        healthBar.maxValue = boss.instance.maxBossHealth;
        healthBar.value = boss.instance.maxBossHealth;
    }
    private void Update()
    {
        healthBar.value = boss.instance.bossHealth;
        if(healthBar.value <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
