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

    public static event Action onBallonDoor;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomCollorSprite();
    }

    void Update()
    {
        transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        onBallonDoor?.Invoke();
        RandomCollorSprite();
        audioSource.Play();
        IncreaseUpSpeed();
        ResetPosition();
    }

    void ResetPosition()
    {
        float randomX = UnityEngine.Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3(randomX, -7f , transform.position.z);
    }

    void RandomCollorSprite()
    {
        int randomSprite = UnityEngine.Random.Range(0, spritesBallon.Length);
        spriteRenderer.sprite = spritesBallon[randomSprite];
    }

    void IncreaseUpSpeed()
    {
        upSpeed += 0.05f;
    }
}
