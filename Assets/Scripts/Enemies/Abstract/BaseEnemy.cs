using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour, IBossMinion
{
    [SerializeField] private UnityEvent OnDeath;
    public Transform healthBarPivot;
    public int damageAmount;
    public Action<GameObject> OnEnemyDie = delegate (GameObject enemy) { };
    public bool canDie = true;
    public float deathDestroyDelay = 1;
    public int maxHealth = 3;
    public int CurrentHealth => damageManager.GetCurrent();
    public Action<StatusAnimation> OnStatusChange = delegate (StatusAnimation status) { };
    private DamageManager damageManager;
    internal bool dead = false;

    event Action<GameObject> IBossMinion.OnDeath
    {
        add
        {
            OnEnemyDie += value;
        }

        remove
        {
            OnEnemyDie -= value;
        }
    }

    private void Awake()
    {
        if (canDie)
            CreateHealthBar();
    }

    internal void StatusDidChange(StatusAnimation status)
    {
        OnStatusChange.Invoke(status);
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
        StatusDidChange(StatusAnimation.dead);
        OnEnemyDie.Invoke(gameObject);
        OnDeath.Invoke();
        Destroy(gameObject, deathDestroyDelay);
    }

    public void SetDamage(GameObject other, int amount = -1)
    {
        float timer = 1f;
        StatusDidChange(StatusAnimation.attacking);
        amount = amount < 0 ? damageAmount : amount;
        if (other.tag == "Player")
        {
            timer = -Time.deltaTime;
            if (timer <= 0)
            {
                var damageController = other.GetComponent<DamageManager>();
                damageController.SetDamage(amount);
            }
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
        {
            damageManager.SetDamage(amount);
            StatusDidChange(StatusAnimation.receiveDamage);
        }
    }

    public void Heal(int amount)
    {
        damageManager.Heal(amount);
    }

    public void BossIsDead()
    {
        damageManager.InstantDeath();
    }    
}
