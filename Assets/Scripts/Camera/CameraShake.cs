using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
     CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

    public float amplitude = 0.13f;
    public float frequency = 0.1f;
    public float initialTimer = 0.3f;
    public float timer;
    public bool makeShake = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = initialTimer;
        _virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (makeShake == true)
        {
            ShakeCamera();
        }
    }

    public void StartShakeCamera()
    {
        makeShake = true;
        timer = initialTimer;
    }

    private void ShakeCamera()
    {
        if (timer > 0)
        {
            _virtualCameraNoise.m_AmplitudeGain = amplitude;
            _virtualCameraNoise.m_FrequencyGain = frequency;
            timer -= Time.deltaTime;
        }
        else
        {
            _virtualCameraNoise.m_AmplitudeGain = 0.0f;
            _virtualCameraNoise.m_FrequencyGain = 0.0f;
            makeShake = false;
        }
    }
}
