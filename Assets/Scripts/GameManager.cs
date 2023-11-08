using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentScore;
    [SerializeField] int increaseValueScore = 10;
    [SerializeField] int highScore;

    public int GetScoreValue() => currentScore;
    public int GetHighScoreValue() => highScore;

    private void OnEnable()
    {
        BalloonController.onBallonDoor += ScoreSystem ;
    }

    private void OnDisable()
    {
        BalloonController.onBallonDoor -= ScoreSystem;
    }

    void ScoreSystem()
    {
        currentScore += increaseValueScore;

        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }

}
