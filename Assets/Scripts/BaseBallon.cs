using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseBallon : MonoBehaviour
{
    [SerializeField] protected float upSpeed;
    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] spritesBallon;


    public bool isFreeze;

    protected void ResetPosition(float yValue)
    {
        float randomX = UnityEngine.Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3(randomX, yValue, transform.position.z);
    }

    protected void BalloonFliesUp()
    {
        if (!isFreeze)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
        }
    }

    protected void IncreaseUpSpeed()
    {
        upSpeed += 0.05f;
    }

}
