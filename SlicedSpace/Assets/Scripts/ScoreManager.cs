using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public Text scoreText;

    int score = 0;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        this.renderScore();
    }

    // Update is called once per frame
    public void addPoint() {
        score += 1;
        this.renderScore();
    }

    private void renderScore() {
        if (score < 10) {
            scoreText.text = "0" + score.ToString();
        } else {
            scoreText.text = score.ToString();
        }
    }
}
