using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] float upSpeed;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] spritesBallon;

    public static event Action OnBalloonPopped;
    public static event Action OnBalloonReachedMaxHeight;
    public static event Action OnBombBalloonExploded;
    public static event Action OnStarBalloonExploded;

    public bool isFreeze;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPosition();
        RandomColorSprite();
    }

    void Update()
    {
        balloonFliesUp();
    }

    private void OnMouseDown()
    {
        OnBalloonPopped?.Invoke();
        audioSource.Play();
        IncreaseUpSpeed();
        ResetPosition();
        RandomColorSprite();
    }

    void balloonFliesUp()
    {
        if (!isFreeze)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);

            if (transform.position.y > 5.2f)
            {
                OnBalloonReachedMaxHeight?.Invoke();
            }
        }
    }

    void ResetPosition()
    {
        float randomX = UnityEngine.Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3(randomX, -7f , transform.position.z);
    }

    void RandomColorSprite()
    {
        int randomSprite = UnityEngine.Random.Range(0, spritesBallon.Length - 1);
        spriteRenderer.sprite = spritesBallon[randomSprite];
    }

    void IncreaseUpSpeed()
    {
        upSpeed += 0.05f;
    }
}
