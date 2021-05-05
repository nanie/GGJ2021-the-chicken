using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Reflection;

public class DamageManager : MonoBehaviour
{
    public Action<int> OnCharacterDamaged = delegate (int currentHealth) { };
    public Action OnCharacterDie = delegate () { };
    public Action<int> OnMaxHealthChange;
    public Func<bool> CanReceiveDamage = delegate () { return true; };
    private int maxHealth = 12;
    private int currentHealth = 12;

    public void SetHealthTotal(int amount)
    {
        maxHealth = amount;
        currentHealth = amount;
        OnMaxHealthChange?.Invoke(amount);
    }
    public void SetDamage(int damageAmount)
    {
        if (!CanReceiveDamage.Invoke())
            return;

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
