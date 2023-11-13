using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentScore;
    [SerializeField] int highScore;
    public TMP_Text comboText;
    [SerializeField] int increaseScoreNormalBallon = 10, increaseScoreStarBallon = 50;

    //[SerializeField] int combo = 0;
    //[SerializeField] float lastScoreTime;

    [SerializeField] GameObject ballonPrefabs;
    [SerializeField] Transform containerBallon;

    public int GetScoreValue() => currentScore;

    [SerializeField] float restartTime;


    private void OnEnable()
    {
        NormalBallon.OnNormalBalloonPopped += delegate { ScoreSystem(increaseScoreNormalBallon); }  ;
        StarBallon.OnStarBallonPopped += delegate { ScoreSystem(increaseScoreStarBallon); };
        NormalBallon.OnNormalBalloonPopped += SpawningBallon;
        NormalBallon.OnBalloonReachedMaxHeight += RestartScene;
        NormalBallon.OnBalloonReachedMaxHeight += RemoveAllBallon;
        BomBallon.OnBomBallonPopped += RemoveAllBallon;
    }

    private void OnDisable()
    {
        NormalBallon.OnNormalBalloonPopped -= delegate { ScoreSystem(increaseScoreNormalBallon); };
        StarBallon.OnStarBallonPopped -= delegate { ScoreSystem(increaseScoreStarBallon); };
        NormalBallon.OnNormalBalloonPopped -= SpawningBallon;
        NormalBallon.OnBalloonReachedMaxHeight -= RestartScene;
        NormalBallon.OnBalloonReachedMaxHeight -= RemoveAllBallon;
        BomBallon.OnBomBallonPopped -= RemoveAllBallon;
    }

    void ScoreSystem(int increaseValueScore)
    {
        currentScore += increaseValueScore;

        if (currentScore > highScore)
        {
           // combo++;
            highScore = currentScore;

            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }

        //lastScoreTime = Time.time;

        //if (combo > 1)
        //{
        //    comboText.text = "Combo x" + combo;
        //}
        //else
        //{
        //    comboText.text = ""; 
        //}
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
            Instantiate(ballonPrefabs, transform.position, Quaternion.identity, containerBallon);
        }
    }

    void RemoveAllBallon()
    {
        StartCoroutine(DelayRemoveAllBallon());
    }

    IEnumerator DelayRemoveAllBallon()
    {
        yield return new WaitForSeconds(2f);
        containerBallon.gameObject.SetActive(false);
    }


    //private void Update()
    //{
    //    if (Time.time - lastScoreTime > 2.0f)
    //    {
    //        combo = 0;
    //        comboText.text = ""; 
    //    }
    //}

}
