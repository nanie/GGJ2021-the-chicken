using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int damageAmount;

    public void SetDamage(GameObject other, int amount = -1)
    {
        amount = amount < 0 ? damageAmount: amount;
        if (other.tag == "Player")
        {
            var damageController = other.GetComponent<PlayerDamage>();
            damageController.SetDamage(amount);
        }
    }

    private void Update()
    {
        CheckAttack();
    }

    public virtual void CheckAttack()
    {
       
    }
}
