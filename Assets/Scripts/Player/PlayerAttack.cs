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
    [SerializeField] private LayerMask attackMask;
    private PlayerController playerController;
    private float holdTime;
    HorizontalDirection direction;
    IAttackSkill selectedSkill;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        direction = playerController.originalSpriteDirection;
    }

    void Update()
    {
        SetDirection();

        if (Input.GetButtonDown(inputButtonNormal))
        {
            NormalAttack();
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
                selectedSkill.UseSkillCharged(transform, attackMask);
            }
            else
            {
                selectedSkill.UseSkillQuick(transform, attackMask);
            }
        }

    }

    private void NormalAttack()
    {
        var hitInfo = Physics2D.OverlapCircleAll(transform.position, attackRadius, attackMask);
        if (hitInfo.Length > 0)
        {
            foreach (var enemy in hitInfo)
            {
                if (enemy.TryGetComponent(out BaseEnemy baseEnemy))
                {
                    baseEnemy.SetDamage(attackPower);
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

    private Vector3 CalculateAttackOffset()
    {
        if (direction == HorizontalDirection.right)
            return transform.position + (Vector3)attackOffsetRight;
        return transform.position + (Vector3)attackOffsetLeft;
    }
}
