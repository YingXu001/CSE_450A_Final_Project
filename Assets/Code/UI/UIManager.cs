using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text ammoText, speedText;
    public float fadeTime;
    public GameObject AmmoUI_1;
    public GameObject AmmoUI_2;
    public GameObject AmmoUI_3;
    public AudioMixer audioMixer;


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
        subController_script = GameObject.FindGameObjectWithTag("Player").GetComponent<SubController>();
        ammoText.text = ": " + 0;
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
        ammoText.text = ": " + subController_script.numAmmo.ToString();
        speedText.text = "Speed: " + subController_script.speedLevel.ToString();
        levelText.text = "Level " + level.ToString();

        if (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            alpha -= fadeSpeed * Time.deltaTime;
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, alpha);
        }else if(fadeTime <= 0){
            Destroy(levelText);
        }

        switch (subController_script.currentBullet)
        {
            case 0: 
                changeAmmoImage(AmmoUI_1);
                break;
            case 1:
                changeAmmoImage(AmmoUI_2);
                break;
            case 2:
                changeAmmoImage(AmmoUI_3);
                break;
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

    private void changeAmmoImage(GameObject ammoImage)
    {
        AmmoUI_1.SetActive(false);
        AmmoUI_2.SetActive(false);
        AmmoUI_3.SetActive(false);

        ammoImage.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


}
