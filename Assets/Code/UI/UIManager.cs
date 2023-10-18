using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject subMarine;
    public Text ammoText;
    private SubController subController_script;
    private int numAmmo;
    [SerializeField] GameObject DeathPanel;
    
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

    public void ToggleDeathPanel()
    {
        DeathPanel.SetActive(!DeathPanel.activeSelf);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void changeSceneByName(string name)
    {
        if(name != null)
        {
            SceneManager.LoadScene(name);
        }
    }
}
