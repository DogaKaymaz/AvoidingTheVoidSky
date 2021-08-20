using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject
        Cam1,
        Cam2;

    private void Start()
    {
        cameraPositionChange(PlayerPrefs.GetInt("0"));
    }

    // void Update()
    // {
    //     switchCamera();
    // }

    public void switchCamera()
    {
            cameraChangeCounter();
    }

    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);

    }

    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }
        
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        if (camPosition == 0)
        {
            Cam1.SetActive(true);
            Cam2.SetActive(false);
        }
        else if (camPosition == 1)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }
    }

}
