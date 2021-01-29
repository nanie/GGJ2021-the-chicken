using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Player movement speed
    private float speed = 5f;

    //Character Rigidy Body
    private Rigidbody2D rigidBody;

    //Horizontal and vertical axis from player input
    [HideInInspector]
    private Vector2 direction;
    [Header("Common settings")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";
    private IAnimatorManager animator;
    [Header("Flip Renderer settings")]
    [SerializeField] private bool flipHorizontal;
    public HorizontalDirection originalSpriteDirection;

    private SpriteRenderer[] spriteRenderers;
    private bool _movementStarted = true;
    private IAttackSkill currentSkill;
    private bool _canWalk = true;
    void Start()
    {
        animator = GetComponentInChildren<IAnimatorManager>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (!_canWalk)
            return;
        //Find input axis
        direction.x = Input.GetAxis(horizontalAxis);
        direction.y = Input.GetAxis(verticalAxis);
        //Flip sprites if needed
        if (flipHorizontal && Mathf.Abs(direction.x) > 0.1)
        {
            foreach (var item in spriteRenderers)
            {
                item.flipX = originalSpriteDirection == HorizontalDirection.left ? direction.x > 0 : direction.x < 0;
            }
        }

        if (animator != null && direction.sqrMagnitude > 0)
        {
            _movementStarted = false;
            animator.SetDirection(direction);
        }
        else if (!_movementStarted)
        {
            animator.SetWalking(false);
            _movementStarted = true;
        }
    }
    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
    }

    public Vector2 GetCurrentDirection()
    {
        return direction;
    }

    public void PlayerCanWalk(bool canWalk)
    {
        _canWalk = canWalk;
    }
}
