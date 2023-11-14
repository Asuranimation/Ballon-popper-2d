using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBallon : BaseBallon 
{
    public static event Action OnBomBallonPopped;
    float randomResetPosition;
    [SerializeField] GameObject particleBom;

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
        OnBomBallonPopped?.Invoke();
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        Instantiate(particleBom , pos, Quaternion.identity);
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
