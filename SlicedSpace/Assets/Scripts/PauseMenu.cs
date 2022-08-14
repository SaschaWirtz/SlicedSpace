using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static PauseMenu P1;

    Button button;

    void Awake (){
        P1 = this;
    }
    
    public void Pause()
    {
        button = GetComponent<Button>();
        button.Select(); 
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
