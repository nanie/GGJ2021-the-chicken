using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Player movement speed
    private float speed = 5f;
    private float potionSpeedBonus;
    private float hatSpeedBonus;
    //Character Rigidy Body
    private Rigidbody2D rigidBody;

    //Horizontal and vertical axis from player input
    [HideInInspector]
    private Vector2 direction;
    [Header("Common settings")]
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
        if (_canWalk)
            rigidBody.MovePosition(rigidBody.position + direction * (speed + potionSpeedBonus) * Time.fixedDeltaTime);
    }

    public Vector2 GetCurrentDirection()
    {
        return direction;
    }

    public void PlayerCanWalk(bool canWalk)
    {
        if (!canWalk)
        {
            direction = new Vector2();
            animator.SetWalking(false);
        }
        _canWalk = canWalk;
    }

    public void SetPotionSpeedBonus(float potionSpeedBonus)
    {
        this.potionSpeedBonus = potionSpeedBonus;
    }

    public void SetHatSpeedBonus(float hatSpeedBonus)
    {
        this.hatSpeedBonus = hatSpeedBonus;
    }

    private void OnMove(InputValue inputValue) // Called when the Move Action from PlayerActions (Input Action element) is performed 
    {
        //Find input axis
        direction.x = inputValue.Get<Vector2>().x;
        direction.y = inputValue.Get<Vector2>().y;

        //Flip sprites if needed
        if (flipHorizontal && Mathf.Abs(direction.x) > 0.1)
        {
            foreach (var item in spriteRenderers)
            {
                item.flipX = originalSpriteDirection == HorizontalDirection.left ? direction.x > 0 : direction.x < 0;
            }
        }
    }
}
