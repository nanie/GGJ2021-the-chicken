using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BaseEnemy enemy;
    bool isAttacking;

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
                animator.SetBool("walk", false);
                break;
            case StatusAnimation.walking:
                animator.SetBool("walk", true);
                break;
            case StatusAnimation.attacking:
                animator.SetTrigger("Attack");
                break;
            case StatusAnimation.dead:
                animator.SetTrigger("dead");
                break;
            case StatusAnimation.charging:
                animator.SetBool("chargingAttack", true);
                break;
            case StatusAnimation.finishCharging:
                animator.SetBool("chargingAttack", false);
                break;
            case StatusAnimation.receiveDamage:
                animator.SetTrigger("receiveDamage");
                break;
        }
    }


}
