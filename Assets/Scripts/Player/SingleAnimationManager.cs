using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationManager : MonoBehaviour, IAnimatorManager
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        DamageManager dgm = GetComponent<DamageManager>();
        dgm.OnCharacterDamaged += CharacterDamaged;
        dgm.OnCharacterDie += CharacterDied;
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        playerAttack.OnPlayerAttack += PlayerAttacked;
    }

    private void PlayerAttacked(bool isCharged, bool isQuick)
    {
        _animator.SetTrigger("Attack");
    }

    private void CharacterDied()
    {
        _animator.SetTrigger("dead");
    }

    private void CharacterDamaged(int obj)
    {
        _animator.SetTrigger("receiveDamage");
    }
    public void Fall()
    {
        _animator.SetTrigger("fall");
    }
    public void SetDirection(Vector2 direction)
    {
        _animator.SetBool("walking", true);
    }

    public void CollectItem()
    {
        _animator.SetTrigger("collect");
    }
    
    public void SetWalking(bool IsWalking)
    {
        _animator.SetBool("walking", IsWalking);
    }
}
