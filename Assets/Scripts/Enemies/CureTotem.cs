using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureTotem : BaseEnemy
{
    [SerializeField] public int healAmount = 1;
    [SerializeField] public float minReuse = 2;
    [SerializeField] public float maxReuse = 4;
    private BossBehaviour boss;
    private float timer;
    void Start()
    {
        boss = FindObjectOfType<BossBehaviour>();
        timer = Random.Range(minReuse, maxReuse);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            boss.Heal(healAmount);
            timer = Random.Range(minReuse, maxReuse);
        }
    }
}
