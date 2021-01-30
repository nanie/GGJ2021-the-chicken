using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private DamageManager playerDamageManager;
    private void OnEnable()
    {
        playerDamageManager.OnCharacterDamaged += SetDamage;
        SetDamage(playerDamageManager.GetCurrent());
    }
    private void SetDamage(int health)
    {
        healthText.text = health.ToString();
    }

   
}
