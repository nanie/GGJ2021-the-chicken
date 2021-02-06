using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerInventoryData")]
public class PlayerInventoryData : ScriptableObject
{
    public InventoryItem[] _items;
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