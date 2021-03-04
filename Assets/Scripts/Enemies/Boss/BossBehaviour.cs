using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

public class BossBehaviour : BaseEnemy
{
    [SerializeField] private BossSkillConfig[] skillConfig;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackTime;
    [SerializeField] private UnityEvent OnBossDie;
    private AIPath aIPath;
    private bool isCharging;
    private Transform player;
    private List<IBossMinion> minions = new List<IBossMinion>();
    float timerAttack;
    private bool isDead = false;
    void Start()
    {
        aIPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AIDestinationSetter aIDestination = GetComponent<AIDestinationSetter>();
        aIDestination.target = player;
        StatusDidChange(StatusAnimation.walking);
        ChooseNextSkill();
        OnEnemyDie += BossDied;
    }

    private void BossDied(GameObject enemy)
    {
        isDead = true;
        foreach (var minion in minions)
        {
            minion.BossIsDead();
        }
        OnBossDie.Invoke();
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
        var config = skillConfig.Where(q => q.health < CurrentHealth).OrderByDescending(q => q.health).First(); // config recebe as skillConfig onde health < que vida atual ordenando da maior health para a menor e pegando o primeiro skillConfig do array resultante (gus)
        StartCoroutine(ExecuteSkill(config.skills[UnityEngine.Random.Range(0, config.skills.Length)])); //executa uma habilidade aleatória q esta no array de config
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
            if (!skill.singleChild || point.childCount == 0)
            {
                var minion = Instantiate(skill.prefab, point.position, point.rotation);
                if (skill.singleChild)
                    minion.transform.SetParent(point);
                var follower = minion.GetComponent<IFollowerMinion>();
                if (follower != null)
                {
                    follower.FollowTarget(player);
                }
                var minionBehaviour = minion.GetComponent<IBossMinion>();
                minions.Add(minionBehaviour);
                minionBehaviour.OnDeath += delegate (GameObject enemy) { MinionDied(minionBehaviour); };
            }

        }
        yield return new WaitForSeconds(1.0f);
        isCharging = false;
        aIPath.canMove = true;
        StatusDidChange(StatusAnimation.finishCharging);
        ChooseNextSkill();
    }

    private void MinionDied(IBossMinion minion)
    {
        if (isDead)
            return;
        if (minions.Contains(minion))
        {
            minions.Remove(minion);
        }
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
    public float skillReuse; // tempo de recarga da habilidade
    public float skillChargeTime; // tempo de conjuração da habilidade
    public bool singleChild;
    public Transform[] spawnPoints;
}