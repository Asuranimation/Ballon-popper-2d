using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentScore;
    [SerializeField] int increaseValueScore = 10;
    [SerializeField] int highScore;

    public TMP_Text comboText;

    [SerializeField] int combo = 0;
    [SerializeField] float lastScoreTime;

    [SerializeField] GameObject ballonPrefabs;

    public int GetScoreValue() => currentScore;
    public int GetHighScoreValue() => highScore;

    [SerializeField] float restartTime;

    BallonType ballonType;

    private void OnEnable()
    {
        BalloonController.OnBalloonPopped += ScoreSystem ;
        BalloonController.OnBalloonPopped += SpawningBallon;
        BalloonController.OnBalloonReachedMaxHeight += RestartScene;
    }

    private void OnDisable()
    {
        BalloonController.OnBalloonPopped -= ScoreSystem;
        BalloonController.OnBalloonPopped -= SpawningBallon;
        BalloonController.OnBalloonReachedMaxHeight -= RestartScene;

    }

    void ScoreSystem()
    {
        currentScore += increaseValueScore;

        if (currentScore > highScore)
        {
            combo++;
            highScore = currentScore;

            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }

        lastScoreTime = Time.time;

        if (combo > 1)
        {
            comboText.text = "Combo x" + combo;
        }
        else
        {
            comboText.text = ""; 
        }
    }

    void RestartScene()
    {
        StartCoroutine(RestartSceneCourotine());
    }

    IEnumerator RestartSceneCourotine()
    {
        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawningBallon()
    {
        if(currentScore % 100 == 0)
        {
            Instantiate(ballonPrefabs);
        }
    }


    private void Update()
    {
        if (Time.time - lastScoreTime > 2.0f)
        {
            combo = 0;
            comboText.text = ""; 
        }
    }

}
