using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static PauseMenu P1;
    GameObject startButtonClicked;
    GameObject exitButtonClicked;
    GameObject startButton;
    GameObject exitButton;
    AudioSource[] sounds;
    AudioSource popcornBag_crumble;
    AudioSource doorOpen_exit;
    bool closePauseMenu = false;
    
    void Start() {
        this.sounds = GetComponents<AudioSource>();
        this.popcornBag_crumble = this.sounds[0];
        this.doorOpen_exit = this.sounds[1];

        this.startButtonClicked = GameObject.Find("start-button-clicked");
        this.exitButtonClicked = GameObject.Find("exit-button-clicked");
        this.exitButton = GameObject.Find("exit-button");
        this.startButton = GameObject.Find("start-button");
        this.startButtonClicked.SetActive(false);
        this.exitButtonClicked.SetActive(false);
    }

    void Awake (){
        P1 = this;
        this.pauseMenu.SetActive(false);
        transform.Find("Resume").GetComponent<Button>().Select();
    }

    void FixedUpdate() {
        if(this.closePauseMenu) {
            this.startButtonClicked.SetActive(false);
            this.exitButtonClicked.SetActive(false);
            this.startButton.SetActive(true);
            this.exitButton.SetActive(true);
            this.closePauseMenu = false;
            pauseMenu.SetActive(false);
        }
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;   
    }

    public void Resume()
    {
        this.ResumeCo();
    }

    async void ResumeCo() {
        this.popcornBag_crumble.Play();
        GameObject.Find("start-button").SetActive(false);
        this.startButtonClicked.SetActive(true);
        await Task.Delay(700);
        this.closePauseMenu = true;
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
        this.HomeCo(sceneID);
    }

    async void HomeCo(int sceneID) {
        this.doorOpen_exit.Play();
        GameObject.Find("exit-button").SetActive(false);
        this.exitButtonClicked.SetActive(true);
        await Task.Delay(700);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID); 
    }
   
}
