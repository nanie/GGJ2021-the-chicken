using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private InventoryItem[] Items;
    [SerializeField] private UseItem useItem;
    public Action<InventoryItem> OnItemChange = delegate (InventoryItem item) { };
    public Action<InventoryItem> OnItemStart = delegate (InventoryItem item) { };
    public bool hasKey;
    private void Start()
    {
        foreach (var item in Items)
        {
            OnItemStart.Invoke(item);
        }
    }

    private void Update()
    {
        foreach (var item in Items)
        {
            if (item.amount > 0 && Input.GetKeyDown(item.keyCode))
            {
                if (useItem.CanUseItemType(item.type))
                {
                    item.amount--;
                    OnItemChange.Invoke(item);
                    useItem.UseItemByType(item.type);
                }
            }
        }
    }
    public void CollectItem()
    {
        CollectItem(ItemType.healthPotion);
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
        else if (type == ItemType.key)
        {
            hasKey = true;
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
    public KeyCode keyCode;
}