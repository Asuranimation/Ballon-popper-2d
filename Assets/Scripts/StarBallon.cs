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
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    if (IsTouched(touch.position))
                    {
                        HandleTouchInput();
                    }
                }
            }
        }
    }

    private bool IsTouched(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }


    private void HandleTouchInput()
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
