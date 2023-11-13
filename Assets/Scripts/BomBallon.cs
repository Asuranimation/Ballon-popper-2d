using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBallon : BaseBallon 
{
    public static event Action OnBomBallonPopped;
    float randomResetPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritesBallon[0];
        ResetPosition(-10f);
    }

    void Update()
    {
        BalloonFliesUp();
        DestroyBallon();
    }

    private void OnMouseDown()
    {
        OnBomBallonPopped?.Invoke();
        audioSource.Play();
        IncreaseUpSpeed();
        ResetPosition(-10f);
    }

    void DestroyBallon()
    {
        if (transform.position.y > 5.2f)
        {
            randomResetPosition = UnityEngine.Random.Range(-8f,-15f);
            ResetPosition(randomResetPosition);
        }
    }
}
