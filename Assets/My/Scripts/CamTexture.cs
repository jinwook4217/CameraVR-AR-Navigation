using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTexture : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private GUITexture cameraTexture;

    void Start()
    {
        cameraTexture = GetComponent<GUITexture>();
        cameraTexture.pixelInset = new Rect(0, 0, Screen.width / 2, Screen.height);
        webcamTexture = new WebCamTexture(1920, 1080, 60);
        webcamTexture.Play();

        cameraTexture.texture = webcamTexture;
    }
}
