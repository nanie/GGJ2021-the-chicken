using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BaseEnemy enemy;

    void Start()
    {
        if (TryGetComponent(out enemy))
        {
            enemy.OnStatusChange += ChangeAnimation;
        }
    }

    private void ChangeAnimation(StatusAnimation status)
    {
        switch (status)
        {
            case StatusAnimation.idle:
                break;
            case StatusAnimation.walking:
                break;
            case StatusAnimation.attacking:
                break;
            case StatusAnimation.dead:
                break;
            case StatusAnimation.charging:
                break;
            case StatusAnimation.receiveDamage:
                break;
        }
    }

}
