using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillAllEnemiesEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllEnemyKilled;
    [SerializeField] private List<BaseEnemy> enemies;
    bool finished = false;
    void Start()
    {
        foreach (var item in enemies)
        {
            item.OnEnemyDie += EnemyKilled;
        }
    }

    private void EnemyKilled(GameObject enemy)
    {
        if (enemy == null)
            return;
        var baseEnemy = enemy.GetComponent<BaseEnemy>();
        if (enemies.Contains(baseEnemy))
        {
            enemies.Remove(baseEnemy);
        }

        if (enemies.Count <= 0 && !finished)
        {
            finished = true;
            OnAllEnemyKilled.Invoke();
        }
    }
}
