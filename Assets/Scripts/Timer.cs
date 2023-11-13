using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float gameTime = 120f; 
    [SerializeField] float timer = 0f;

    void Start()
    {
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while (timer < gameTime )
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }

}
