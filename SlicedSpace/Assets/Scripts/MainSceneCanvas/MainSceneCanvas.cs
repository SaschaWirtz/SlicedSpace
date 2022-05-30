using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainSceneCanvas : MonoBehaviour
{
    GameObject[] uiElements;

    // Start is called before the first frame update
    void Start()
    {
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
        Application.Quit();
    }

    public void startGame() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
