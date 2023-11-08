using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameManager gameManager;
    float highscore;

    private void OnEnable()
    {
        BalloonController.onBallonDoor += ShowTextScore;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highScore",0);
        highScoreText.text = highscore.ToString();
    }

    private void OnDisable()
    {
        BalloonController.onBallonDoor += ShowTextScore;
    }

    void ShowTextScore()
    {
        currentScoreText.text = gameManager.GetScoreValue().ToString();
    }

}
