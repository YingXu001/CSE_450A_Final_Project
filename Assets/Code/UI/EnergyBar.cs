using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Slider energyBar;
    private SubController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SubController>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = player.energyMax;
        energyBar.value = 0;
    }
    public void SetEnergy(int hp)
    {
        energyBar.value = hp;
    }
}
