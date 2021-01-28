using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    private void OnEnable()
    {
        var pd = FindObjectOfType<PlayerDamage>();
        pd.OnPlayerDamaged += DoDamageEffect;
    }

    private void DoDamageEffect(int obj)
    {
        if (cameraShake != null)
        {
            cameraShake.StartShakeCamera();
        }
    }
}
