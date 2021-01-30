using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private int powerPotionPower = 1;
    [SerializeField] private int durationPotionPower = 4;

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

    private void UseLightHat()
    {
        throw new NotImplementedException();
    }

    private void UseSpeedHat()
    {
        throw new NotImplementedException();
    }

    private void UseLinkHat()
    {
        throw new NotImplementedException();
    }

    private void UseSpeedPotion()
    {
        throw new NotImplementedException();
    }

    private void UseHealthPotion()
    {
        throw new NotImplementedException();
    }
}
