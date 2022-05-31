using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;

    private int score = 0;
    private int lifes = 10;
    private float[] lifeFillAmounts = new float[11]{0.14f,0.23f,0.3f,0.4f,0.47f,0.57f,0.64f,0.74f,0.84f,0.92f,1f};
    private const int MAX_LIFES = 10;


    private ScoreManager() {

    }

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

    public void loseLife() {
        if (this.lifes > 0) {
            --this.lifes;
        }
        
        this.renderLifes();
    }

    public void extraLife() {
        if (this.lifes < ScoreManager.MAX_LIFES) {
            ++this.lifes;
        }

        this.renderLifes();
    }

    public bool isGameOver() {
        return this.lifes < 1;
    }

    private void renderScore() {
        if (score < 10) {
            scoreText.text = "0" + score.ToString();
        } else {
            scoreText.text = score.ToString();
        }
    }

    private void renderLifes() {
        GameObject
            .Find("Lifes")
            .GetComponent<Image>().fillAmount = this.lifeFillAmounts[this.lifes];
    }
}
