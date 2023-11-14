using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NormalBallon : BaseBallon
{
    public static event Action OnNormalBalloonPopped;
    public static event Action OnBalloonReachedMaxHeight;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPosition(-7f);
        RandomColorSprite();
    }

    void Update()
    {
        BalloonFliesUp();
        DestroyBallon();
        OnTouchBallon();
    }

    void OnTouchBallon()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        BallonPopped();
                    }
                }
            }
        }
    }


    private void BallonPopped()
    {
        OnNormalBalloonPopped?.Invoke(); // to Gm
        audioSource.Play();
        IncreaseUpSpeed();
        ResetPosition(-7f);
        RandomColorSprite();
    }

    void RandomColorSprite()
    {
        int randomSprite = UnityEngine.Random.Range(0, spritesBallon.Length -2);
        spriteRenderer.sprite = spritesBallon[randomSprite];
    }

    void DestroyBallon()
    {
        if (transform.position.y > 5.2f)
        {
            OnBalloonReachedMaxHeight?.Invoke(); //ke Gm and UIManager
            isFreeze = true;
        }
    }
   
}
