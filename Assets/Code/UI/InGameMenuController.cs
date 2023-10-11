using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public static InGameMenuController instance;
    public GameObject pauseMenu;
    public GameObject controlMenu;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hide();
    }

    void SwitchMenu(GameObject someMenu)
    {
        pauseMenu.SetActive(false);
        controlMenu.SetActive(false);

        someMenu.SetActive(true);   
    }

    public void ShowPauseMenu()
    {
        SwitchMenu(pauseMenu);
    }

    public void ShowControlMenu()
    {
        SwitchMenu(controlMenu);
    }
    
    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        ShowPauseMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
        SubController.instance.isPaused = true;
    }

    public void Hide() 
    {

        Debug.Log("Clicked");
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if(SubController.instance != null)
        {
            SubController.instance.isPaused = false;
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
