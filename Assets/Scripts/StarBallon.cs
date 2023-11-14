using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBallon : BaseBallon
{
    public static event Action OnStarBallonPopped;
    float randomResetPosition;
    [SerializeField] GameObject particleStar;
 
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
        OnStarBallonPopped?.Invoke();
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        Instantiate(particleStar, pos, Quaternion.identity);
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
