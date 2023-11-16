using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    //Outlets
    public Transform[] ammoSpawns;
    public Transform shieldSpawn;
    public Transform powerSpawn;
    public Transform exitSpawn;

    public GameObject ammo;
    public GameObject shield;
    public GameObject power;
    public GameObject exit;

    public GameObject finalBoss;
    public int healthThreshold;

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        healthThreshold = finalBoss.GetComponent<FinalBossEnemy>().bossHealth/2;
    }

    private void Update()
    {
        if(finalBoss.GetComponent<FinalBossEnemy>().bossHealth <= healthThreshold)
        {
            spawnResources();
            healthThreshold = 0;
        }
        if(finalBoss.GetComponent<FinalBossEnemy>() == null) {
            Instantiate(exit, exitSpawn.position, Quaternion.identity);
        }
    }

    void spawnResources()
    {
        
        foreach (Transform t in ammoSpawns) {
            Instantiate(ammo, t.position, Quaternion.identity);
        }

        Instantiate(shield, shieldSpawn.position, Quaternion.identity);
        Instantiate(power, powerSpawn.position, Quaternion.identity);
        
    }

}
