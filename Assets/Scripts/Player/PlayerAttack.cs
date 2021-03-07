using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackPower;
    [SerializeField] private Vector2 attackOffsetRight;
    [SerializeField] private Vector2 attackOffsetLeft;
    [SerializeField] private string inputButtonNormal = "Fire1";
    [SerializeField] private string inputButtonSpecial = "Fire2";
    [SerializeField] private string inputButtonDodge = "Fire3";
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float dodgeTime = 0.5f;
    public Action<bool, bool> OnPlayerAttack = delegate (bool isCharged, bool isQuick) { };
    public Action OnPlayerDodge = delegate () { };
    private PlayerController playerController;
    private float holdTime;
    private HorizontalDirection direction;
    private int attackPowerBonus = 0;
    private IAttackSkill selectedSkill;
    private float timer;
    private DamageManager _damageManager;
    bool _dodging = false;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        _damageManager = GetComponent<DamageManager>();
        direction = playerController.originalSpriteDirection;
        _damageManager.CanReceiveDamage += delegate () { return !_dodging; };
    }

    void Update()
    {
        
        timer -= Time.deltaTime;

        SetDirection();

        if (_dodging)
            return;

        if (Input.GetButtonDown(inputButtonNormal) && timer <= 0)
        {
            timer = attackSpeed;
            OnPlayerAttack.Invoke(false, false);
            NormalAttack();
        }

        if (Input.GetButtonDown(inputButtonDodge) && timer <= 0)
        {
            timer = attackSpeed;
            OnPlayerDodge.Invoke();
            StartCoroutine(Dodge());
        }

        if (selectedSkill == null)
            return;

        if (Input.GetButton(inputButtonSpecial))
        {
            holdTime += Time.deltaTime;
            playerController.PlayerCanWalk(false);
            selectedSkill.ShowVisualFeedback(transform, holdTime);
        }
        if (Input.GetButtonUp(inputButtonSpecial))
        {
            playerController.PlayerCanWalk(true);
            if (selectedSkill != null && holdTime > selectedSkill.ChargeTime())
            {
                OnPlayerAttack.Invoke(true, false);
                selectedSkill.UseSkillCharged(transform, attackMask);
            }
            else
            {
                OnPlayerAttack.Invoke(true, true);
                selectedSkill.UseSkillQuick(transform, attackMask);
            }
            holdTime = 0;
        }

    }

    IEnumerator Dodge()
    {
        _dodging = true;
        yield return new WaitForSeconds(dodgeTime);
        _dodging = false;
    }

    private void NormalAttack()
    {
        var hitInfo = Physics2D.OverlapCircleAll(CalculateAttackOffset(), attackRadius, attackMask);
        if (hitInfo.Length > 0)
        {
            foreach (var enemy in hitInfo)
            {
                if (enemy.TryGetComponent(out BaseEnemy baseEnemy))
                {
                    baseEnemy.SetDamage(attackPower + attackPowerBonus);
                }
            }
        }
    }

    private void SetDirection()
    {
        var horizontalAxis = playerController.GetCurrentDirection().x;
        if (horizontalAxis > 0)
            direction = HorizontalDirection.right;
        else if (horizontalAxis < 0)
            direction = HorizontalDirection.left;
    }
    
    private void OnDrawGizmosSelected()
    {
        
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CalculateAttackOffset(), attackRadius);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + (Vector3)attackOffsetRight, attackRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position + (Vector3)attackOffsetLeft, attackRadius);
        }
    }

    public void SetBonus(int amount)
    {
        attackPowerBonus = amount;
    }
    private Vector3 CalculateAttackOffset()
    {
        if (direction == HorizontalDirection.right)
        {
            return transform.position + new Vector3(attackOffsetRight.x, attackOffsetRight.y, 0);
        }
        return transform.position + new Vector3(attackOffsetLeft.x, attackOffsetLeft.y, 0);
    }

    
}
