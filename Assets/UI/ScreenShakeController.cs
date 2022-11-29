using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class ScreenShakeController : MonoBehaviour
{
    public enum ShakeType { Random }
    private float shakeTimer = 0, shakePower = 0, shakeFadeTimer = 0;
    private ShakeType shakeType;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    #region Public functions
    public void StartShake(float time, float power, ShakeType type = ShakeType.Random)
    {
        Debug.Log(time + " " + power);
        if (power > shakePower)
        {
            shakeTimer = time;
            shakePower = power;
            shakeType = type;

            shakeFadeTimer = power / time;
        }
    }

    public void StopShake()
    {
        shakeTimer = 0;
    }
    #endregion

    #region Unity Functions
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            switch (shakeType)
            {
                case (ShakeType.Random):
                    RandomShake();
                    break;
                default:
                    break;
            }

            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
    #endregion

    #region Shake Functions
    private void RandomShake()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakePower;

        shakePower = Mathf.MoveTowards(shakePower, 0, shakeFadeTimer * Time.deltaTime);
    }
    #endregion
}
