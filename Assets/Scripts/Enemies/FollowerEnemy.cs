using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class FollowerEnemy : BaseEnemy
{
    internal Transform target;
    internal bool following = false;
    [SerializeField]
    internal float attackDistance = 0.2f;
    [SerializeField]
    private float attackTime = 1f;
    AIPath aI;

    private float timerAttack;

    void Start()
    {
        timerAttack = attackTime;
        OnEnemyDie += Died;
        aI = GetComponent<AIPath>();
    }

    private void Died()
    {
        aI.canMove = false;
    }

    public override void CheckAttack()
    {
        if (dead)
            return;
        if (following == true && Vector2.Distance(target.position, transform.position) <= attackDistance)
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                timerAttack = attackTime;
                SetDamage(target.gameObject);
            }
        }
    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartFollow(collision.transform);
        }
    }

    internal void StartFollow(Transform target)
    {
        this.target = target;
        following = true;
        AIDestinationSetter aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = target;
    }
}
