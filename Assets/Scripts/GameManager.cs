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
        BomBallon.OnBomBallonPopped += RestartScene;
        NormalBallon.OnBalloonReachedMaxHeight += RemoveAllBallon;
        BomBallon.OnBomBallonPopped += RemoveAllBallon;
    }

    private void OnDisable()
    {
        NormalBallon.OnNormalBalloonPopped -= delegate { ScoreSystem(increaseScoreNormalBallon); };
        StarBallon.OnStarBallonPopped -= delegate { ScoreSystem(increaseScoreStarBallon); };
        NormalBallon.OnNormalBalloonPopped -= SpawningBallon;
        NormalBallon.OnBalloonReachedMaxHeight -= RestartScene;
        BomBallon.OnBomBallonPopped -= RestartScene;
        NormalBallon.OnBalloonReachedMaxHeight -= RemoveAllBallon;
        BomBallon.OnBomBallonPopped -= RemoveAllBallon;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }



    void ScoreSystem(int increaseValueScore)
    {
        currentScore += increaseValueScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;

            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
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
        Vector2 posSpawn = new Vector2 (transform.position.x, -10);
        if(currentScore % 100 == 0 || currentScore % 70 == 0)
        {
            Instantiate(ballonPrefabs, posSpawn, Quaternion.identity, containerBallon);
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

}
