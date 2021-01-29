using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class FireAbility : FollowerEnemy
{
    public float radius;
    public float distance;
    public LayerMask layer;
     float timer;
    public float reuseTime;
    AIPath aIPath;
    bool charging = false;
    public float chargeTime;
    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        timer = reuseTime;
    }

    public override void CheckAttack()
    {
        if (charging) // se carregando "poder carregar a habilidade especial"
        {
            CheckChargingEnd();
        }

        else if(timer <= 0)
        {
            
            if (following == true && Vector2.Distance(target.position, transform.position) <= attackDistance)
            {
                timer = chargeTime;
                charging = true;                // recebe o tempo que demora para conjurar a habilidade especial
                aIPath.canMove = false;

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
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, radius, transform.right, distance, 3);
        if (hitInfo)
        {
            SetDamage(hitInfo.transform.gameObject);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
