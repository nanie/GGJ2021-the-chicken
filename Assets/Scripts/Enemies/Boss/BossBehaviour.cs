using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBehaviour : BaseEnemy
{
    [SerializeField] private BossSkillConfig[] skillConfig;
    private AIPath aIPath;
    private bool isCharging;

    void Start()
    {
        aIPath = GetComponent<AIPath>();
        ChooseNextSkill();
    }

    public override void CheckAttack()
    {
        if (isCharging)
            return;

        base.CheckAttack();
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
            Instantiate(skill.prefab, point.position, point.rotation);
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