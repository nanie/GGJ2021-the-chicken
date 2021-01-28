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
    public float timer = 1.0f;
    public bool makeShake = false;
    // Start is called before the first frame update
    void Start()
    {
        _virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShakeCamera();
        }
        if (makeShake == true)
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

    public void ShakeCamera()
    {
        makeShake = true;
        timer = 1.0f;
    }
}
