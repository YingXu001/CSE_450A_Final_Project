using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject subMarine;
    public Text ammoText;
    private SubController subController_script;
    private int numAmmo;
    
    // Start is called before the first frame update
    void Start()
    {
        subController_script = subMarine.GetComponent<SubController>();
        ammoText.text = "Ammo: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + subController_script.numAmmo.ToString();
    }

}
