using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LockedDoor : MonoBehaviour
{
    [SerializeField] private PositionSmoothAnimation doorAnimator;
    [SerializeField] private GameObject needKeyVisualFeedback;
    [SerializeField] private GameObject canOpenVisualFeedback;
    [SerializeField] private InputActionReference OpenDoorInputAction;
    bool isOpen = false;
    bool canOpen;
    private PlayerInventoryController playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canOpen = playerInventory.hasKey;
            if(canOpen)
            {
                canOpenVisualFeedback.SetActive(true);
            }
            else
            {
                needKeyVisualFeedback.SetActive(true);
            }         
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canOpenVisualFeedback.SetActive(false);
        needKeyVisualFeedback.SetActive(false);
    }
    private void Update()
    {
        if (isOpen)
            return;
        if (canOpen)
            if (OpenDoorInputAction.action.triggered)
                OpenDoor();
    }

    private void OpenDoor()
    {
        canOpenVisualFeedback.SetActive(false);
        needKeyVisualFeedback.SetActive(false);
        doorAnimator.Animate();
    }
}
