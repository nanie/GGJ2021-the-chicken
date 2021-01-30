using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemElement : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private TextMeshProUGUI textIndex;
    [SerializeField] private GameObject itemNotDiscovered;

    public void SetData(Sprite icon, int amount, int index, bool discovered)
    {
        index = (index + 1) & 10;
        textIndex.text = index.ToString();
        imgIcon.sprite = icon;
        textAmount.text = $"x{amount}";
        if (amount <= 0)
        {
            imgIcon.color = new Color(255, 255, 255, 0.5f);
        }

        imgIcon.gameObject.SetActive(discovered);
        itemNotDiscovered.gameObject.SetActive(!discovered);

    }

    public void SetAmount(int amount)
    {
        textAmount.text = $"x{amount}";
        if (amount <= 0)
        {
            imgIcon.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            imgIcon.color = new Color(255, 255, 255, 1f);
            imgIcon.gameObject.SetActive(true);
            itemNotDiscovered.gameObject.SetActive(false);
        }
    }
}
