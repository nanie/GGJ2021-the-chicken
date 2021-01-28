using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationManager : MonoBehaviour, IAnimatorManager
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetDirection(Vector2 direction)
    {
        _animator.SetBool("walking", true);
    }

    public void SetWalking(bool IsWalking)
    {
        _animator.SetBool("walking", IsWalking);
    }
}
