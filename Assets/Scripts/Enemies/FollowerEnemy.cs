using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class FollowerEnemy : BaseEnemy, IFollowerMinion
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

    private void Died(GameObject enemy)
    {
        aI.canMove = false;
    }

    public override void CheckAttack()
    {
        if (target == null) // se não tem nenhum alvo
        {
            StatusDidChange(StatusAnimation.idle); // idle 
        }
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
        else if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) > attackDistance && following == true)
            {
                StatusDidChange(StatusAnimation.walking);
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
        
        AIDestinationSetter aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = target;
        following = true;
        StatusDidChange(StatusAnimation.walking);
    }

    public void FollowTarget(Transform target)
    {
        StartFollow(target);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        var labelPos = transform.position + new Vector3(0, attackDistance + 0.2f, 0);
        Handles.Label(labelPos, "Attack Radius", style);
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
#endif
}
