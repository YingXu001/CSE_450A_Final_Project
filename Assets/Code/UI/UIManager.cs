using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject subMarine;
    public Text ammoText, speedText;
    public float fadeTime;
    private float alpha;
    private float fadeSpeed;
    public TextMeshProUGUI levelText;
    private int level;
    private SubController subController_script;
    [SerializeField] GameObject DeathPanel;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        subController_script = subMarine.GetComponent<SubController>();
        ammoText.text = "Ammo: " + 0;
        speedText.text = "Speed: " + 0;

        //fade away level message
        fadeSpeed = 1/fadeTime;
        levelText.text = "Level " + 0;
        alpha = levelText.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        ammoText.text = "Ammo: " + subController_script.numAmmo.ToString();
        speedText.text = "Speed: " + subController_script.speedLevel.ToString();
        levelText.text = "Level " + level.ToString();

        if(fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            alpha -= fadeSpeed * Time.deltaTime;
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, alpha);
        }
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
