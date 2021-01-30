using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private int powerPotionPower = 1;
    [SerializeField] private int durationPotionPower = 4;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float speedPotionPower = 2f;
    [SerializeField] private float speedHatPower = 3f;
    [SerializeField] private int speedPotionDuration = 4;
    [SerializeField] private GameObject[] galinhas;
    bool hasAttackBonus;
    bool hasSpeedBonus;

    
    public bool CanUseItemType(ItemType type)
    {
        switch (type)
        {
            case ItemType.speedPotion:
                return !hasSpeedBonus;
            case ItemType.attackPower:
                return !hasAttackBonus;
        }
        return true;
    }
    public void UseItemByType(ItemType type)
    {
        switch (type)
        {
            case ItemType.healthPotion:
                UseHealthPotion();
                break;
            case ItemType.attackPower:
                UseAttackPotion();
                break;
            case ItemType.speedPotion:
                UseSpeedPotion();
                break;
            case ItemType.link_hat:
                UseLinkHat();
                break;
            case ItemType.speed_hat:
                UseSpeedHat();
                break;
            case ItemType.light_hat:
                UseLightHat();
                break;
        }
    }

    IEnumerator UseAttackPotion()
    {
        playerAttack.SetBonus(powerPotionPower);
        hasAttackBonus = true;
        yield return new WaitForSeconds(durationPotionPower);
        playerAttack.SetBonus(0);
        hasAttackBonus = false;
    }
    IEnumerator UseSpeedPotion()
    {
        playerController.SetPotionSpeedBonus(speedPotionPower);
        hasSpeedBonus = true;
        yield return new WaitForSeconds(speedPotionDuration);
        playerController.SetPotionSpeedBonus(0f);
        hasSpeedBonus = false;
    }

    private void UseHealthPotion()
    {
        throw new NotImplementedException();
    }
    private void UseLightHat()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 2)
            {
                galinhas[i].SetActive(true);
            }
            else
            {
                galinhas[i].SetActive(false);
            }
        }
    }

    private void UseSpeedHat()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                galinhas[i].SetActive(true);
            }
            else
            {
                galinhas[i].SetActive(false);
            }
        }
        playerController.SetHatSpeedBonus(speedHatPower);
        
    }

    private void UseLinkHat()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 1)
            {
                galinhas[i].SetActive(true);
            }
            else
            {
                galinhas[i].SetActive(false);
            }
        }
    }

    
}
