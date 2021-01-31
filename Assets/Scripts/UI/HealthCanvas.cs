using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class HealthCanvas : MonoBehaviour
{
    //TODO aplicara a barra de corações para os outros códigos
    [SerializeField] private DamageManager playerDamageManager;
    public Image[] healthImage;
    public Sprite[] heartSprites;
    private int fullHeartSpriteAmount = 0;
    private int partialHeartSpriteAmount = 0;
    private void OnEnable()
    {
        playerDamageManager.OnCharacterDamaged += SetDamage;
        SetDamage(playerDamageManager.GetCurrent());
    }
    private void SetDamage(int health)
    {
        
        fullHeartSpriteAmount = playerDamageManager.GetCurrent() / 4;
        partialHeartSpriteAmount = playerDamageManager.GetCurrent() % 4;
        for (int i = 0; i < fullHeartSpriteAmount; i++)
        {
            healthImage[i].sprite = heartSprites[0];
        }

        if (partialHeartSpriteAmount > 0)
        {
            if (partialHeartSpriteAmount == 1)
            {
                healthImage[fullHeartSpriteAmount].sprite = heartSprites[3];
            }
            if (partialHeartSpriteAmount == 2)
            {
                healthImage[fullHeartSpriteAmount].sprite = heartSprites[2];
            }
            if (partialHeartSpriteAmount == 3)
            {
                healthImage[fullHeartSpriteAmount].sprite = heartSprites[1];
            }
        }

        for (int i = fullHeartSpriteAmount; i + 1 <= healthImage.Length; i++)
        {
            if (partialHeartSpriteAmount == 0)
            {
                healthImage[fullHeartSpriteAmount].sprite = heartSprites[4];
            }
            else if (i + 1 < healthImage.Length)
            {
                healthImage[fullHeartSpriteAmount + 1].sprite = heartSprites[4];
            }
            else if (playerDamageManager.GetCurrent() == 0)
            {
                healthImage[fullHeartSpriteAmount].sprite = heartSprites[4];
            }
        }
    }

   
}
