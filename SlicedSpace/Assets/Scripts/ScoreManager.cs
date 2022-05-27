using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public Text scoreText;

    int score = 00;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        
    }

    // Update is called once per frame
    public void addPoint() {
        score += 1;
        if (score<10){
            scoreText.text = "0" + score.ToString();
        }else
        scoreText.text = score.ToString();
    }
}
