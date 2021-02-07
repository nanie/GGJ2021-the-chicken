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
    [SerializeField] private MovingParticle _movingParticle;
    [SerializeField] private Transform _playerItemCollectPivot;
    [SerializeField] private RectTransform _ItemCollectedPivot;
    [SerializeField] private CollectItemParticle _collectItemParticle;

    public Action<InventoryItem, InventoryItem, InventoryItem> OnSelectItem = delegate (InventoryItem item, InventoryItem nextItem, InventoryItem previousItem) { };
    public Action<InventoryItem, InventoryItem, InventoryItem> OnUpdateItem = delegate (InventoryItem item, InventoryItem nextItem, InventoryItem previousItem) { };
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
            var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
            var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            OnSelectItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
        }
    }
    private void Update()
    {
        if (_discoveredItems.Count < 1)
            return;

        if (Input.GetKeyDown(KeyCode.U))
        {
            _currentIndex = (_currentIndex + 1) % _discoveredItems.Count;
            var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
            var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            _currentItem = _discoveredItems[_currentIndex].type;
            OnSelectItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            _currentIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
            var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            _currentItem = _discoveredItems[_currentIndex].type;
            OnSelectItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (_discoveredItems[_currentIndex].amount > 0 && useItem.CanUseItemType(_currentItem))
            {
                _discoveredItems[_currentIndex].amount--;
                useItem.UseItemByType(_currentItem);
                var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
                var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
                OnUpdateItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
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
            else
                _collectItemParticle.ShowItem(_playerItemCollectPivot.position, item.icon);

            item.discovered = true;
            _discoveredItems.Add(item);
            _movingParticle.StartAnimationMovingTarget(_playerItemCollectPivot.position, _ItemCollectedPivot, item.particleColor.colorKeys);
            var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
            var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
            OnUpdateItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
        }
        else if (type == ItemType.key)
        {
            animationManager.CollectItem(_keyIcon);
            hasKey = true;
            OnKeyStatusChange.Invoke(hasKey);
        }

    }
}