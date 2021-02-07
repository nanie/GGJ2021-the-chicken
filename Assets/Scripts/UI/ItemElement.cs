using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemElement : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image nextIcon;
    [SerializeField] private Image previousIcon;
    [SerializeField] private TextMeshProUGUI textAmount;

    private void SetData(Sprite icon, int amount)
    {
        imgIcon.gameObject.SetActive(true);
        textAmount.gameObject.SetActive(true);
        imgIcon.sprite = icon;
        textAmount.text = $"x{amount}";
        if (amount <= 0)
        {
            imgIcon.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            imgIcon.color = new Color(255, 255, 255, 1f);
        }
    }

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
    }
}
