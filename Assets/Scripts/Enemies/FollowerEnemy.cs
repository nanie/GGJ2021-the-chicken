using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class FollowerEnemy : BaseEnemy
{
    internal Transform target;
    internal bool following = false;
    [SerializeField]
    internal float attackDistance = 0.2f;
    [SerializeField]
    private float attackTime = 1f;
  
    
    private float timerAttack;
   
    // Start is called before the first frame update
    void Start()
    {
      timerAttack = attackTime;
      
}

    // Update is called once per frame
    public override void CheckAttack()
    {
        if(following == true && Vector2.Distance(target.position, transform.position) <= attackDistance)
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                timerAttack = attackTime;
                SetDamage(target.gameObject);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartFollow(collision.transform);
        }
    }

    void StartFollow(Transform target)
    {
        this.target = target;
        following = true;
        AIPath aI = GetComponent<AIPath>(); 
        AIDestinationSetter aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = target;

        
    }
}
