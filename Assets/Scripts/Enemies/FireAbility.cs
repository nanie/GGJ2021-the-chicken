using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class FireAbility : FollowerEnemy
{
    public float radius;
    public int chargedAttackPower;
    public LayerMask layer;
    float timer;
    public float reuseTime;
    AIPath aIPath;
    bool charging = false;
    public float chargeTime;
    public bool isStatic;
    
    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        if (isStatic == true)
        {
            aIPath.canMove = false;
        }else
        {
            aIPath.canMove = true;
        }
        timer = reuseTime;
    }

    public override void CheckAttack()
    {
        if (charging) // se carregando "poder carregar a habilidade especial"
        {
            CheckChargingEnd();
        }
        else if (timer <= 0)
        {
            if (following == true && Vector2.Distance(target.position, transform.position) <= attackDistance)
            {
                StatusDidChange(StatusAnimation.charging);
                timer = chargeTime;
                charging = true;                // recebe o tempo que demora para conjurar a habilidade especial
                if (!isStatic)
                {
                    aIPath.canMove = false;
                }
            }
        }
        else // ataque normal
        {
            timer -= Time.deltaTime;
            base.CheckAttack();
        }
    }

    private void CheckChargingEnd()
    {
        timer -= Time.deltaTime; // conjurando a habilidade especial
        if (timer <= 0) // se carregou
        {
            Shoot(); // usa a habilidade especial
            timer = reuseTime; // coloca em tempo de recarga a habilidade especial
            charging = false;
            if (!isStatic)
            {
                aIPath.canMove = true;
            }
        }
    }

    void Shoot()
    {
        var hitInfo = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        if (hitInfo.Length > 0)
        {            
            SetDamage(hitInfo[0].transform.gameObject, chargedAttackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
