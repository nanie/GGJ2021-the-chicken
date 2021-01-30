using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public Action<int> OnCharacterDamaged = delegate (int currentHealth) { };
    public Action OnCharacterDie = delegate () { };
    private int maxHealth = 10;
    private int currentHealth = 10;
    public void SetHealthTotal(int amount)
    {
        maxHealth = amount;
        currentHealth = amount;
    }
    private void Start()
    {
        
    }
    public void SetDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            OnCharacterDie.Invoke();
        }
        else
        {
            OnCharacterDamaged.Invoke(currentHealth);
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        OnCharacterDamaged.Invoke(currentHealth);
    }
    public int GetCurrent()
    {
        return currentHealth;
    }

    public void InstantDeath()
    {
        currentHealth = 0;
        OnCharacterDie.Invoke();
    }
}
