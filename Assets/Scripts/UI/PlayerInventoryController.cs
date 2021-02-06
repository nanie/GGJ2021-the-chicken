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
    [SerializeField] private Sprite _keyIcon;

    public Action<InventoryItem> OnSelectItem = delegate (InventoryItem item) { };
    public Action<InventoryItem> OnUpdateItem = delegate (InventoryItem item) { };
    public Action<bool> OnKeyStatusChange = delegate (bool hasKey) { };
    public bool hasKey;
    private ItemType _currentItem;
    private int _currentIndex;
    private List<InventoryItem> _discoveredItems;
    private void Start()
    {
        _discoveredItems = _inventory.Items.Where(q => q.discovered).ToList();
        if (_discoveredItems.Count > 0)
        {
            _currentItem = _discoveredItems[_currentIndex].type;
            OnSelectItem.Invoke(_discoveredItems[_currentIndex]);
        }
    }
    private void Update()
    {
        if (_discoveredItems.Count < 1)
            return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            _currentIndex = (_currentIndex + 1) % _discoveredItems.Count;
            _currentItem = _discoveredItems[_currentIndex].type;
            OnSelectItem.Invoke(_discoveredItems[_currentIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            _currentIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            _currentItem = _discoveredItems[_currentIndex].type;
            OnSelectItem.Invoke(_discoveredItems[_currentIndex]);
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            if (_discoveredItems[_currentIndex].amount > 0 && useItem.CanUseItemType(_currentItem))
            {
                _discoveredItems[_currentIndex].amount--;
                useItem.UseItemByType(_currentItem);
                OnUpdateItem.Invoke(_discoveredItems[_currentIndex]);
            }
        }
    }

    public void UseKey()
    {
        if (hasKey)
        {
            hasKey = true;
            OnKeyStatusChange.Invoke(hasKey);
        }
    }

    public void CollectItem(ItemType type)
    {
        if (_inventory.Items.Any(q => q.type == type))
        {
            var item = _inventory.Items.Where(q => q.type == type).First();
            item.amount++;
            if (!item.discovered)
                animationManager.CollectItem(item.icon);
            item.discovered = true;
            _discoveredItems.Add(item);

            if (_currentItem == type)
                OnUpdateItem.Invoke(_discoveredItems[_currentIndex]);
        }
        else if (type == ItemType.key)
        {
            animationManager.CollectItem(_keyIcon);
            hasKey = true;
            OnKeyStatusChange.Invoke(hasKey);
        }
       
    }
}