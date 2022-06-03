using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreText : MonoBehaviour {

    public Text gameOverScoreText;

    private Text text;

	
	void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {
        text.text = "" + PlayerMovement.SCORE;
        gameOverScoreText.text = "" + PlayerMovement.SCORE;
	}
}
