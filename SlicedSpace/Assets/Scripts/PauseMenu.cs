using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] 
    GameObject pauseMenu;
    public static PauseMenu P1;
    GameObject startButtonClicked;
    GameObject exitButtonClicked;
    GameObject startButton;
    GameObject exitButton;
    AudioSource[] sounds;
    AudioSource popcornBag_crumble;
    AudioSource doorOpen_exit;
    bool closePauseMenu = false;
    private float lastVolume;
    public AudioMixer mixer;
    
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
        this.mixer.GetFloat("MasterVolume", out float tempLastVolume);
        Debug.Log(Mathf.Pow(10, tempLastVolume / 20));
        this.lastVolume = Mathf.Pow(10, tempLastVolume / 20);
        this.mixer.SetFloat("MasterVolume", Mathf.Log10(this.lastVolume / 2) * 20);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;   
    }

    public void Resume()
    {
        StartCoroutine(ResumeCo());
    }

    IEnumerator ResumeCo() {
        this.mixer.SetFloat("MasterVolume", Mathf.Log10(this.lastVolume) * 20);
        this.popcornBag_crumble.Play();
        GameObject.Find("start-button").SetActive(false);
        this.startButtonClicked.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        this.closePauseMenu = true;
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
        StartCoroutine(HomeCo(sceneID));
    }

    IEnumerator HomeCo(int sceneID) {
        this.mixer.SetFloat("MasterVolume", Mathf.Log10(this.lastVolume) * 20);
        this.doorOpen_exit.Play();
        GameObject.Find("exit-button").SetActive(false);
        this.exitButtonClicked.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID); 
    }
   
}
