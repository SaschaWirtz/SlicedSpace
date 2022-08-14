using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainSceneCanvas : MonoBehaviour
{
    GameObject[] uiElements;
    GameObject startButtonClicked;
    GameObject exitButtonClicked;
    AudioSource[] sounds;
    AudioSource popcornBag_crumble;
    AudioSource doorOpen_exit;



    // Start is called before the first frame update
    void Start()
    {
        this.sounds = GetComponents<AudioSource>();
        this.popcornBag_crumble = this.sounds[0];
        this.doorOpen_exit = this.sounds[1];

        this.startButtonClicked = GameObject.Find("start-button-clicked");
        this.exitButtonClicked = GameObject.Find("exit-button-clicked");
        this.startButtonClicked.SetActive(false);
        this.exitButtonClicked.SetActive(false);

        if (GlobalInformation.getInstance().introPlayed) {
            GameObject
                .Find("IntroPlayer")
                .GetComponent<VideoPlayer>().Stop();
        } else {
            uiElements = GameObject.FindGameObjectsWithTag("UIElement");
            foreach(GameObject uiElement in uiElements) {
                uiElement.SetActive(false);
            }
            
            GameObject
                .Find("IntroPlayer")
                .GetComponent<VideoPlayer>()
                .loopPointReached += (VideoPlayer vp) => {
                    foreach (GameObject uiElement in uiElements) {
                        uiElement.SetActive(true);
                    }

                    GlobalInformation.getInstance().introPlayed = true;
                };
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void exitGame() {
        StartCoroutine(exit());
    }

    IEnumerator exit() {
        this.doorOpen_exit.Play();
        GameObject.Find("exit-button").SetActive(false);
        this.exitButtonClicked.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Application.Quit();
    }

    public void startGame() {
        StartCoroutine(start());
    }

    IEnumerator start() {
        this.popcornBag_crumble.Play();
        GameObject.Find("start-button").SetActive(false);
        this.startButtonClicked.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void credits(){
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
