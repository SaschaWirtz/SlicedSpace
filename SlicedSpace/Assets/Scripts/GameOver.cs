using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] 
    GameObject gameOver;
    Button button;
    
    public void tryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }
}
