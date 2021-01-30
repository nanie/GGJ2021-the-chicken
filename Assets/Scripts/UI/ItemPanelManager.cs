using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelManager : MonoBehaviour
{
    [SerializeField] private ItemElement itemElementPrefab;
    [SerializeField] private Transform itemElementParent;
    private PlayerInventoryController inventory;
    private Dictionary<ItemType, ItemElement> itemElements = new Dictionary<ItemType, ItemElement>();
    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventoryController>();
        inventory.OnItemStart += StartItem;
        inventory.OnItemChange += ChangeItem;
    }

    private void ChangeItem(InventoryItem obj)
    {
        itemElements[obj.type].SetAmount(obj.amount);
    }

    private void StartItem(InventoryItem obj)
    {
        var newElement = Instantiate(itemElementPrefab, itemElementParent);
        newElement.SetData(obj.icon, obj.amount, itemElements.Count, obj.discovered);
        itemElements.Add(obj.type, newElement);
    }
}
