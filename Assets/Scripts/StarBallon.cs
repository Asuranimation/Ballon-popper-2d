using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBallon : BaseBallon
{
    public static event Action OnStarBallonPopped;
    float randomResetPosition;
 
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPosition(-15f);
        spriteRenderer.sprite = spritesBallon[0];
    }


    private void Update()
    {
        BalloonFliesUp();
        DestroyBallon();
    }

 
    private void OnMouseDown()
    {
        OnStarBallonPopped?.Invoke();
        audioSource.Play();
        IncreaseUpSpeed();
        randomResetPosition = UnityEngine.Random.Range(-15, -30);
        ResetPosition(randomResetPosition);
    }



    void DestroyBallon()
    {
        if (transform.position.y > 5.2f)
        {
            ResetPosition(randomResetPosition);
        }
    }
}
