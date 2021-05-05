using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class NearInteraction : MonoBehaviour
{
    public UnityEvent OnInteract;
    [SerializeField] InputActionReference NearInteractionInputAction;
    [SerializeField] private bool SingleInteraction = true;
    bool canInteract = false;
    private void Awake()
    {
        NearInteractionInputAction.action.Enable();
    }
    private void Update()
    {
        if (!canInteract)
            return;
        
        if (NearInteractionInputAction.action.triggered)
        {
            OnInteract.Invoke();
            if (SingleInteraction)
            {
                canInteract = false;
                Destroy(this);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
    }

}
