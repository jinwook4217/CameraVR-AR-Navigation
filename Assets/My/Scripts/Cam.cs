using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    WebCamTexture webCam = null;

    void Start()
    {
        StartCoroutine(StartWebcam());
    }

    IEnumerator StartWebcam()
    {
        webCam = new WebCamTexture(1920, 1080, 60);
        webCam.Play();

        while (!webCam.didUpdateThisFrame) yield return null;

        GetComponent<Renderer>().material.mainTexture = webCam;

        float sy = 2.0f * Screen.width / Screen.height;
        float sx = sy * webCam.width / webCam.height;
        transform.localScale = new Vector3(sx, sy, 1);
    }

    void OnDestroy()
    {
        if (webCam != null)
        {
            webCam.Stop();
        }
        StopAllCoroutines();
    }
}
