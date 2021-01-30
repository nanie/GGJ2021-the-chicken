using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NearInteraction : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private bool SingleInteraction = true;
    bool canInteract = false;

    private void Update()
    {
        if (!canInteract)
            return;

        if (Input.GetButtonDown("Fire1"))
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
