using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {

    public Text gameOverText;
    public Text gameOverScoreText;

    private Text text;
    private bool isNewRecord = false;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = "" + PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {
        if (PlayerMovement.SCORE > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", PlayerMovement.SCORE);
            isNewRecord = true;
            text.text = "" + PlayerPrefs.GetInt("HighScore");
            gameOverText.text = "New Record!";
            gameOverText.color = Color.green;
            gameOverScoreText.color = Color.green;
        }
    }

    public bool GetIsNewRecord() {
        return isNewRecord;
    }


}
