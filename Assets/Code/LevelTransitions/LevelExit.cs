using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SubController>())
        {
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene("VictoryScreen");
                Cursor.lockState = CursorLockMode.None;

            }
            else { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            }
            
        }
    }
}
