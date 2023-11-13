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
