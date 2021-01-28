using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Action<int> OnPlayerDamaged = delegate (int currentHealth) { };
    public Action OnPlayerDie = delegate () { };

    private int currentHealth = 10;
    private void Start()
    {
        OnPlayerDamaged.Invoke(currentHealth);
    }
    public void SetDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            OnPlayerDie.Invoke();
        }
        else
        {
            OnPlayerDamaged.Invoke(currentHealth);
        }
    }
}
