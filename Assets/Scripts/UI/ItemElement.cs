using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemElement : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI textAmount;

    public void SetData(Sprite icon, int amount)
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

    public void SetAmount(int amount)
    {
        imgIcon.gameObject.SetActive(true);
        textAmount.gameObject.SetActive(true);
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
}
