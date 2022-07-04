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

    // Start is called before the first frame update
    void Start()
    {
        this.startButtonClicked = GameObject.Find("start-button-clicked");
        this.exitButtonClicked = GameObject.Find("exit-button-clicked");
        this.startButtonClicked.SetActive(false);
        this.exitButtonClicked.SetActive(false);

        if (GlobalInformation.getInstance().introPlayed) {
            GameObject
                .Find("IntroPlayer")
                .GetComponent<VideoPlayer>().Stop();

            GameObject
                .Find("IntroPlayer")
                .GetComponent<GameObject>().SetActive(false);
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
        GameObject.Find("exit-button").SetActive(false);
        this.exitButtonClicked.SetActive(true);
        new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void startGame() {
        GameObject.Find("start-button").SetActive(false);
        this.startButtonClicked.SetActive(true);
        new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
