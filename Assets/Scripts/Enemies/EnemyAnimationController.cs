using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : FollowerEnemy
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
                break;
            case StatusAnimation.walking:
                if (animator.GetBool("attack") == true) // se estava atacando
                {
                    animator.SetBool("attack", false); // para de atacar -> começa a andar
                }
                else
                {
                    animator.SetBool("walk", true); 
                }
                break;
            case StatusAnimation.attacking:
                animator.SetBool("attack", true);
                break;
            case StatusAnimation.dead:
                animator.SetTrigger("dead");
                break;
            case StatusAnimation.charging:
                animator.SetTrigger("chargingAttack");
                break;
            case StatusAnimation.receiveDamage:
                animator.SetTrigger("receiveDamage");
                break;
        }
    }

   
}
