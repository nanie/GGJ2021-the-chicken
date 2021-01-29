using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public Transform healthBarPivot;
    public int damageAmount;
    public Action OnEnemyDie = delegate () { };
    public bool canDie = true;
    public float deathDestroyDelay = 1;
    public int maxHealth = 3;
    private DamageManager damageManager;
    internal bool dead = false;
    private void Awake()
    {
        if (canDie)
            CreateHealthBar();
    }

    private void CreateHealthBar()
    {
        damageManager = GetComponent<DamageManager>();
        if (damageManager == null)
            damageManager = gameObject.AddComponent<DamageManager>();
        damageManager.OnCharacterDie += EnemyDied;
        damageManager.SetHealthTotal(maxHealth);

        var barController = FindObjectOfType<EnemyBarController>();

        if (barController != null)
        {
            if (healthBarPivot == null)
            {
                healthBarPivot = transform;
            }
            barController.CreateBar(damageManager, healthBarPivot);
        }
    }

    private void EnemyDied()
    {
        dead = true;
        OnEnemyDie.Invoke();
        Destroy(gameObject, deathDestroyDelay);
    }

    public void SetDamage(GameObject other, int amount = -1)
    {
        amount = amount < 0 ? damageAmount : amount;
        if (other.tag == "Player")
        {
            var damageController = other.GetComponent<DamageManager>();
            damageController.SetDamage(amount);
        }
    }

    private void Update()
    {
        if (dead)
            return;
        CheckAttack();
    }

    public virtual void CheckAttack()
    {

    }

    public void SetDamage(int amount)
    {
        if (canDie)
            damageManager.SetDamage(amount);
    }
}
