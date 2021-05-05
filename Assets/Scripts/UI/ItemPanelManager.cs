using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelManager : MonoBehaviour
{
    [SerializeField] private ItemElement currentItem;
    private PlayerInventoryController inventory;
    [SerializeField] private GameObject keyItem;
    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventoryController>();
        inventory.OnSelectItem += SelectItem;
        inventory.OnUpdateItem += SelectItem;
        inventory.OnKeyStatusChange += ShowKey;
    }

    private void ShowKey(bool show)
    {
        keyItem.SetActive(show);
    }

    private void SelectItem(Sprite icon, int amount, int inventorySlotIndex)
    {
        currentItem.SetData( icon,  amount,  inventorySlotIndex);
    }
}
