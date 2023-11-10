using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photographer : MonoBehaviour
{
    public WebCamTexture webCamTexture;
    [SerializeField] RawImage rawCamTexture;
    [SerializeField] RawImage hasilImage;
    Texture2D photoTexture2D;

    public Button btnCapture;
    public Button btnDone;

    [SerializeField] GameObject photoCanvas;
    [SerializeField] BalloonController balloonController;



    private void Awake()
    {
        btnCapture.onClick.AddListener(() => TakePhoto());
        btnDone.onClick.AddListener(() => Done());
    }
    void Start()
    {
        webCamTexture =  new WebCamTexture();
        webCamTexture.Play();
        rawCamTexture.texture = webCamTexture;
    }

    void TakePhoto()
    {
        photoTexture2D = new Texture2D(webCamTexture.width, webCamTexture.height);
        photoTexture2D.SetPixels(webCamTexture.GetPixels());
        photoTexture2D.Apply();
        hasilImage.texture = photoTexture2D;
        webCamTexture.Pause();
    }

    void Done()
    {
        photoCanvas.SetActive(false);
        webCamTexture.Stop();
        balloonController.isFreeze = false;
    }

    public Texture2D SavePhoto()
    {
        if(photoTexture2D == null)
        {
            return photoTexture2D;
        }
        return null;
    }

}
