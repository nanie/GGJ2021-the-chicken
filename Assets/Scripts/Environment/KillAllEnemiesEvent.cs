using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillAllEnemiesEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllEnemyKilled;
    [SerializeField] private List<BaseEnemy> enemies;
    int count;
    void Start()
    {
        count = enemies.Count;
        foreach (var item in enemies)
        {
            item.OnEnemyDie += EnemyKilled;
        }
    }

    private void EnemyKilled()
    {
        count--;
        if (count <= 0)
        {
            OnAllEnemyKilled.Invoke();
            enabled = false;
        }
    }
}
