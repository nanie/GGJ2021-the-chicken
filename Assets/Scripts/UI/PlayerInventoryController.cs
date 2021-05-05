using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInventoryController : MonoBehaviour
{

    [SerializeField] private PlayerInventoryData _inventory;
    [SerializeField] private UseItem useItem;
    [SerializeField] private SingleAnimationManager animationManager;
    [SerializeField] private Sprite _keyIcon;
    [SerializeField] private MovingParticle _movingParticle;
    [SerializeField] private Transform[] _playerItemCollectPivot;
    [SerializeField] private RectTransform _ItemCollectedPivot;
    [SerializeField] private CollectItemParticle _collectItemParticle;
    [SerializeField] private InputActionReference[] UsePotionInputActions;
    [SerializeField] private List<InventoryItem> itemsInTheInventory;
    public Action<Sprite, int, int> OnSelectItem = delegate (Sprite icon, int amount, int inventorySlotIndex) { };
    public Action<Sprite, int, int> OnUpdateItem = delegate (Sprite icon, int amount, int inventorySlotIndex) { };
    public Action<bool> OnKeyStatusChange = delegate (bool hasKey) { };
    public bool hasKey;
    private ItemType _currentItem;
    private int _currentIndex;
    private List<InventoryItem> _discoveredItems;
    private void Start()
    {
        _discoveredItems = _inventory.Items.Where(q => q.discovered).ToList();
        itemsInTheInventory = _discoveredItems.Where(q => q.amount > 0).ToList();
        if (_discoveredItems.Count > 0)
        {
            for (int i = 0; i < itemsInTheInventory.Count; i++)
            {
                OnSelectItem.Invoke(itemsInTheInventory[i].icon, itemsInTheInventory[i].amount, i);
                Debug.Log(i);
            }
        }
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.U))
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
        else if (UsePotionInputAction[].action.triggered)
        {

            if (_discoveredItems[_currentIndex].amount > 0 && useItem.CanUseItemType(_currentItem))
            {
                _discoveredItems[_currentIndex].amount--;
                useItem.UseItemByType(_currentItem);
                var nextItemIndex = (_currentIndex + 1) % _discoveredItems.Count;
                var previousItemIndex = (_currentIndex - 1) < 0 ? _discoveredItems.Count - 1 : (_currentIndex - 1);
                OnUpdateItem.Invoke(_discoveredItems[_currentIndex], _discoveredItems[nextItemIndex], _discoveredItems[previousItemIndex]);
            }
        }*/

        if (_discoveredItems.Count < 1)
            return;

        foreach (InputActionReference inputActionReference in UsePotionInputActions)
        {
            if (inputActionReference.action.triggered)
            {
                int itemIndex = Array.IndexOf(UsePotionInputActions, inputActionReference);

                if (itemsInTheInventory.Exists(x => itemsInTheInventory.IndexOf(x) == itemIndex))
                {

                    if (itemsInTheInventory[itemIndex].icon != null && useItem.CanUseItemType(itemsInTheInventory[itemIndex].type))
                    {
                        itemsInTheInventory[itemIndex].amount--;

                        useItem.UseItemByType(itemsInTheInventory[itemIndex].type);

                        OnSelectItem.Invoke(itemsInTheInventory[itemIndex].icon, itemsInTheInventory[itemIndex].amount, itemIndex);
                        if (itemsInTheInventory[itemIndex].amount <= 0)
                        {
                            itemsInTheInventory[itemIndex] = null;
                            
                        }
                    }
                }
                break;
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
            int inventorySlotIndex;
            var item = _inventory.Items.Where(q => q.type == type).First();
            item.amount++;

            if (itemsInTheInventory.Any(q => q == item) == true)
            {
                var existingItem = itemsInTheInventory.First(q => q == item);
                existingItem.amount++;
                inventorySlotIndex = itemsInTheInventory.IndexOf(itemsInTheInventory.First(q => q == item));
            }
            else
            {
                Debug.Log(itemsInTheInventory.Any(q => q == null));
                if (itemsInTheInventory.Any(q => q.icon == null))
                {
                    inventorySlotIndex = itemsInTheInventory.IndexOf(itemsInTheInventory.Where(q => q.icon == null).First());
                    itemsInTheInventory[inventorySlotIndex] = item;
                    
                }
                else
                {
                    inventorySlotIndex = Array.IndexOf(_playerItemCollectPivot, _playerItemCollectPivot.Where(x => x.GetComponent<InventorySlot>().isOccupied == false).First());
                    itemsInTheInventory.Add(item);
                }
            }

            if (!item.discovered)
            {
                animationManager.CollectItem(item.icon);
                item.discovered = true;
                _discoveredItems.Add(item);
            }
            else
            {
                _collectItemParticle.ShowItem(_playerItemCollectPivot[inventorySlotIndex].position, item.icon);
            }

            _movingParticle.StartAnimationMovingTarget(_playerItemCollectPivot[inventorySlotIndex].position, _ItemCollectedPivot, item.particleColor.colorKeys);

            OnUpdateItem.Invoke(itemsInTheInventory[inventorySlotIndex].icon, itemsInTheInventory[inventorySlotIndex].amount, inventorySlotIndex);
            
        }
        else if (type == ItemType.key)
        {
            animationManager.CollectItem(_keyIcon);
            hasKey = true;
            OnKeyStatusChange.Invoke(hasKey);
        }

    }
}