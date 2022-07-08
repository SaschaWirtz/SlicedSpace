using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;

    private int score;
    private int maxScore;
    private int lifes = 10;
    private float[] lifeFillAmounts = new float[11]{0.14f,0.23f,0.3f,0.4f,0.47f,0.57f,0.64f,0.74f,0.84f,0.92f,1f};
    private const int MAX_LIFES = 10;


    private ScoreManager() {

    }

    void Update() {
        if(Input.GetButtonDown("Disable/Enable Map")) {
            GameObject.Find("BorderMask").GetComponent<Image>().enabled = !GameObject.Find("BorderMask").GetComponent<Image>().isActiveAndEnabled;
            GameObject.Find("MapMask").GetComponent<Image>().enabled = !GameObject.Find("MapMask").GetComponent<Image>().isActiveAndEnabled;
            GameObject.Find("MiniMapBorder").GetComponent<Image>().enabled = !GameObject.Find("MiniMapBorder").GetComponent<Image>().isActiveAndEnabled;
            GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = !GameObject.Find("MiniMap").GetComponent<RawImage>().isActiveAndEnabled;
            print("efgh");
        }
    }

    private void Awake() {
        instance = this;
        this.maxScore = GameObject.FindGameObjectsWithTag("Butter").Length;
        this.score = 0;
    }
    // Start is called before the first frame update
    void Start() {
        this.renderScore();
    }

    // Update is called once per frame
    public void addPoint() {
        this.score += 1;
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
        string scoreLeftDisplay = this.score.ToString();
        string maxScoreDisplay = this.maxScore.ToString();
        if(this.score < 10) {
            scoreLeftDisplay = "0" + this.score.ToString();
        }
        if(this.maxScore < 10) {
            maxScoreDisplay = "0" + this.maxScore.ToString();
        }
        this.scoreText.text = scoreLeftDisplay + " / " + maxScoreDisplay;
    }

    private void renderLifes() {
        GameObject
            .Find("Lifes")
            .GetComponent<Image>().fillAmount = this.lifeFillAmounts[this.lifes];
    }
    
    public bool requirementCheck() {
        return this.maxScore == this.score;
    }
}
