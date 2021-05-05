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
    public Action<Sprite , int , int > OnSelectItem = delegate (Sprite icon, int amount, int inventorySlotIndex) { };
    public Action<Sprite, int, int> OnUpdateItem = delegate (Sprite icon, int amount, int inventorySlotIndex) { };
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
            for (int i = 0; i < _discoveredItems.Count; i++)
            {
                OnSelectItem.Invoke(_discoveredItems[i].icon, _discoveredItems[i].amount, i);
                
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
        
        foreach (InputActionReference inputActionReference in UsePotionInputActions) {
            if (inputActionReference.action.triggered)
            {
                int itemIndex = Array.IndexOf(UsePotionInputActions, inputActionReference);
                
                if (_discoveredItems.Exists(x => _discoveredItems.IndexOf(x) == itemIndex))
                {
                   
                    if (_discoveredItems[itemIndex].amount > 0 && useItem.CanUseItemType(_discoveredItems[itemIndex].type))
                    {
                        _discoveredItems[itemIndex].amount--;
                        Debug.Log("Usei Item");
                        useItem.UseItemByType(_discoveredItems[itemIndex].type);
                        
                        OnSelectItem.Invoke(_discoveredItems[itemIndex].icon, _discoveredItems[itemIndex].amount, itemIndex);
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
            int inventorySlotIndex = Array.IndexOf(_playerItemCollectPivot, _playerItemCollectPivot.Where(x => x.GetComponent<InventorySlot>().isOccupied == false).First());
            var item = _inventory.Items.Where(q => q.type == type).First();
            item.amount++;
            if (!item.discovered)
                animationManager.CollectItem(item.icon);
            else
                _collectItemParticle.ShowItem(_playerItemCollectPivot[inventorySlotIndex].position, item.icon);

            item.discovered = true;
            _discoveredItems.Add(item);
            _movingParticle.StartAnimationMovingTarget(_playerItemCollectPivot[inventorySlotIndex].position, _ItemCollectedPivot, item.particleColor.colorKeys);
            OnUpdateItem.Invoke(_discoveredItems[inventorySlotIndex].icon, _discoveredItems[inventorySlotIndex].amount, inventorySlotIndex);
        }
        else if (type == ItemType.key)
        {
            animationManager.CollectItem(_keyIcon);
            hasKey = true;
            OnKeyStatusChange.Invoke(hasKey);
        }

    }
}