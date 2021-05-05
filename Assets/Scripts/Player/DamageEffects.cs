using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private DamageManager playerDamageMagager;
    private void OnEnable()
    {
        playerDamageMagager.OnCharacterDamaged += DoDamageEffect;
    }

    private void DoDamageEffect(int obj)
    {
        if (cameraShake != null)
        {
            cameraShake.StartShakeCamera();
        }

    }
}
