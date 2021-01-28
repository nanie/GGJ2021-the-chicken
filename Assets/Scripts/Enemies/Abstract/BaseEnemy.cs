using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int damageAmount;

    public void SetDamage(GameObject other)
    {
        if(other.tag == "Player")
        {
            var damageController = other.GetComponent<PlayerDamage>();
            damageController.SetDamage(damageAmount);
        }
    }
}
