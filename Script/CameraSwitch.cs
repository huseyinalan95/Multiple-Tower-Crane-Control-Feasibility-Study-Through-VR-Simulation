using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject SecondCamera;

    AudioListener MainCameraAudioLis;
    AudioListener SecondCameraAudioLis;

    void Start()
    {
        MainCameraAudioLis = MainCamera.GetComponent<AudioListener>();
        SecondCameraAudioLis = SecondCamera.GetComponent<AudioListener>();

        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }

    void Update()
    {
        switchCamera();
    }

    public void cameraPositionM()
    {
        cameraChangeCounter();
    }

    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            cameraChangeCounter();
        }
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
            MainCamera.SetActive(true);
            MainCameraAudioLis.enabled = true;

            SecondCameraAudioLis.enabled = false;
            SecondCamera.SetActive(false);
        }

        if (camPosition == 1)
        {
            SecondCamera.SetActive(true);
            SecondCameraAudioLis.enabled = true;

            MainCameraAudioLis.enabled = false;
            MainCamera.SetActive(false);
        }
    }
}