using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private PlayerInventoryData _inventory;
    [SerializeField] private UseItem useItem;
    [SerializeField] private SingleAnimationManager animationManager;
    public Action<InventoryItem> OnItemChange = delegate (InventoryItem item) { };
    public Action<InventoryItem> OnItemStart = delegate (InventoryItem item) { };
    public bool hasKey;
    private void Start()
    {
        foreach (var item in _inventory._items)
        {
            OnItemStart.Invoke(item);
        }
    }

    private void Update()
    {
        foreach (var item in _inventory._items)
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
        if (_inventory._items.Any(q => q.type == type))
        {
            var item = _inventory._items.Where(q => q.type == type).First();
            item.amount++;
            item.discovered = true;
            OnItemChange.Invoke(item);
        }
        else if (type == ItemType.key)
        {
            animationManager.CollectItem();
            hasKey = true;
        }
    }
}