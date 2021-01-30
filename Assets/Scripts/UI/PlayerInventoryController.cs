using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private InventoryItem[] Items;
    public Action<InventoryItem> OnItemChange = delegate (InventoryItem item) { };
    public Action<InventoryItem> OnItemStart = delegate (InventoryItem item) { };
    private void Start()
    {
        foreach (var item in Items)
        {
            OnItemStart.Invoke(item);
        }
    }

    public void CollectItem(ItemType type)
    {
        if (Items.Any(q => q.type == type))
        {
            var item = Items.Where(q => q.type == type).First();
            item.amount++;
            item.discovered = true;
            OnItemChange.Invoke(item);
        }
    }
}

[Serializable]
public class InventoryItem
{
    public ItemType type;
    public int amount;
    public bool discovered;
    public Sprite icon;
}