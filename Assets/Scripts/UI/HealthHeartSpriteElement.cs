using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Essa classe controla um único coração
public class HealthHeartSpriteElement : MonoBehaviour
{
    public Image healthImage;
    public Sprite[] heartSprites;

    public void SetValue(int amount)
    {
        if (amount >= 0 && amount < heartSprites.Length)
            healthImage.sprite = heartSprites[amount];
    }

    public void SetMax()
    {
        healthImage.sprite = heartSprites[heartSprites.Length - 1];
    }

    public void SetEmpty()
    {
        healthImage.sprite = heartSprites[0];
    }
}
