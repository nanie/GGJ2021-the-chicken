using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> OnEnter;
    [SerializeField] private UnityEvent<Collider2D> OnExit;
    [SerializeField] private UnityEvent<Collider2D> OnStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnStay.Invoke(collision);
    }

}
