using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Text trueHeadingTxt = null;

    float trueHeading = 0f;
    float destinationAngle = 0f;

    Vector3 myPos = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 destinationPos = Vector3.zero;

    GameObject cameraAxis = null;
    GameObject guideAxis = null;

    void Start()
    {
        Input.location.Start();
        Input.compass.enabled = true;

        cameraAxis = GameObject.FindGameObjectWithTag("CameraAxis");
        guideAxis = GameObject.FindGameObjectWithTag("GuideAxis");

        SetDestination(37.559597f, 127.043733f);

        StartCoroutine(CameraAxisRotate());
    }

    void Update()
    {
        /*
        if (!Input.location.isEnabledByUser || Input.location.status == LocationServiceStatus.Failed)
        {
            return;
        }
        */

        trueHeading = Input.compass.trueHeading;

        trueHeadingTxt.text = "TrueHeading : " + trueHeading;
    }

    IEnumerator CameraAxisRotate()
    {
        Quaternion currentHeading;

        while (true)
        {
            currentHeading = Quaternion.Euler(0f, trueHeading, 0f);

            cameraAxis.transform.rotation = Quaternion.Slerp(cameraAxis.transform.rotation,
                currentHeading, Time.deltaTime * 2f);

            yield return null;
        }
    }

    public void SetDestination(float latitude, float longitude)
    {
        destinationPos.Set(longitude * 100, 0f, latitude * 100);
        calculateDirection();
    }

    public void UpdateMyPosition(float latitude, float longitude)
    {
        myPos.Set(longitude * 100, 0f, latitude * 100);
        calculateDirection();
    }

    void calculateDirection()
    {
        direction = destinationPos - myPos;
        direction.Normalize();

        destinationAngle = Vector3.Angle(Vector3.forward, direction);
        destinationAngle = Vector3.Dot(Vector3.right, direction) > 0.0 ? destinationAngle : -destinationAngle;

        guideAxis.transform.rotation = Quaternion.Euler(0f, destinationAngle, 0f);
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
