using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] float upSpeed;
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        score++;
        audioSource.Play();
        ResetPosition();
    }

    void ResetPosition()
    {
        float randomX = Random.Range(-2.5f, 2.5f);

        transform.position = new Vector3(randomX, -7f , transform.position.z);
    }
}
