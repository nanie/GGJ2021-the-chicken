using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private ItemType item;
    [SerializeField] private Sprite openSprite;
    private PlayerInventoryController playerInventory;
    private NearInteraction interaction;
    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
        interaction = GetComponent<NearInteraction>();
        interaction.OnInteract.AddListener(Open);
    }

    private void Open()
    {
        playerInventory.CollectItem(item);
        GetComponentInChildren<SpriteRenderer>().sprite = openSprite;
    }
}
