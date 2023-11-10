using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI canvasGameOverHighscoreText;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject gameOver;
    [SerializeField] AudioClip gameoverclip;
    [SerializeField] bool isGameOver = false;
    float highscore;

    private void OnEnable()
    {
        BalloonController.OnBalloonPopped += ShowTextScore;
        BalloonController.OnBalloonReachedMaxHeight += ShowGameOver;
    }
    private void OnDisable()
    {
        BalloonController.OnBalloonPopped += ShowTextScore;
        BalloonController.OnBalloonReachedMaxHeight -= ShowGameOver;

    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highScore",0);
        highScoreText.text = highscore.ToString();
    }


    void ShowTextScore()
    {
        currentScoreText.text = gameManager.GetScoreValue().ToString();
    }

    void ShowGameOver()
    {
        if (!isGameOver)
        {
            gameOver.SetActive(true);
            canvasGameOverHighscoreText.text = "highscore : \n " + highscore.ToString();
            highScoreText.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(gameoverclip,Camera.main.transform.position);
            isGameOver = true;
        }
    }

}
