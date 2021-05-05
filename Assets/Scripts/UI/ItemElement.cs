using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemElement : MonoBehaviour
{
    
    [SerializeField] private Image nextIcon;
    [SerializeField] private Image previousIcon;
    [SerializeField] private TextMeshProUGUI[] textAmount;
    [SerializeField] private Image[] inventorySlots;
    [SerializeField] private Sprite defaultSprite;
    public void SetData(Sprite icon, int amount, int inventorySlotIndex)
    {

        if (amount <= 0)
        {
            inventorySlots[inventorySlotIndex].sprite = defaultSprite;
            inventorySlots[inventorySlotIndex].GetComponent<InventorySlot>().isOccupied = false;
            textAmount[inventorySlotIndex].gameObject.SetActive(false);
        }
        else
        {
            textAmount[inventorySlotIndex].gameObject.SetActive(true);
            textAmount[inventorySlotIndex].SetText(amount.ToString());
            inventorySlots[inventorySlotIndex].sprite = icon;
            inventorySlots[inventorySlotIndex].GetComponent<InventorySlot>().isOccupied = true;

        }
    }
       // textAmount.text = $"x{amount}";
        
        
    
/*
    public void SetData(Sprite icon, int amount, Sprite next, Sprite previous)
    {
        if(next != icon)
        {
            nextIcon.transform.parent.gameObject.SetActive(true);
            nextIcon.sprite = next;
            previousIcon.transform.parent.gameObject.SetActive(true);
            previousIcon.sprite = previous;
        }   
        SetData(icon, amount);
    }*/
}
