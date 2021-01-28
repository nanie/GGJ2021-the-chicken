using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        var pd = FindObjectOfType<PlayerDamage>();
        pd.OnPlayerDamaged += SetDamage;
    }
    private void SetDamage(int health)
    {
        healthText.text = health.ToString();
    }

   
}
