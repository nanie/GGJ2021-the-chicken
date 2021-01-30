using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBehaviour : BaseEnemy
{
    [SerializeField] private BossSkillConfig[] skillConfig;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackTime;
    private AIPath aIPath;
    private bool isCharging;
    private Transform player;
    float timerAttack;
    void Start()
    {
        aIPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AIDestinationSetter aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = player;
        ChooseNextSkill();
    }

    public override void CheckAttack()
    {
        if (isCharging)
            return;

        if (Vector2.Distance(player.position, transform.position) <= attackDistance)
        {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0)
            {
                timerAttack = attackTime;
                SetDamage(player.gameObject);
            }
        }
    }

    private void ChooseNextSkill()
    {
        var config = skillConfig.Where(q => q.health < CurrentHealth).OrderBy(q => q.health).First();
        StartCoroutine(ExecuteSkill(config.skills[UnityEngine.Random.Range(0, config.skills.Length)]));
    }

    IEnumerator ExecuteSkill(BossSkill skill)
    {
        yield return new WaitForSeconds(skill.skillReuse);
        StatusDidChange(StatusAnimation.charging);
        isCharging = true;
        aIPath.canMove = false;
        yield return new WaitForSeconds(skill.skillChargeTime);     
        foreach (var point in skill.spawnPoints)
        {
            var minion = Instantiate(skill.prefab, point.position, point.rotation);
            var follower = minion.GetComponent<IFollowerMinion>();
            if(follower!=null)
            {
                follower.FollowTarget(player);
            }
        }
        yield return new WaitForSeconds(1.0f);
        isCharging = false;
        aIPath.canMove = true;
        StatusDidChange(StatusAnimation.walking);
        ChooseNextSkill();
    }
}
[Serializable]
public class BossSkillConfig
{
    public int health;
    public BossSkill[] skills;
}

[Serializable]
public class BossSkill
{
    public GameObject prefab;
    public float skillReuse;
    public float skillChargeTime;
    public Transform[] spawnPoints;
}