using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static PauseMenu P1;

    void Awake (){
        P1 = this;
        this.pauseMenu.SetActive(false);
        transform.Find("Resume").GetComponent<Button>().Select();
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;   
        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneID); 
    }
   
}
