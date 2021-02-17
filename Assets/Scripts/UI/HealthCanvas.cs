using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private DamageManager playerDamageManager;
    //Esse prefab contem um controlador de 1 coração
    [SerializeField] private HealthHeartSpriteElement _heartPrefab;
    [SerializeField] private Transform _heartParentPanel;

    private List<HealthHeartSpriteElement> _elementList = new List<HealthHeartSpriteElement>();

    private int fullHeartSpriteAmount = 0;
    private int partialHeartSpriteAmount = 0;
    private const int HEART_SIZE = 4;
    private void OnEnable()
    {
        playerDamageManager.OnCharacterDamaged += SetDamage;
        playerDamageManager.OnMaxHealthChange += SetMaxHealth;

        for (int i = 0; i < playerDamageManager.GetCurrent() / HEART_SIZE; i++)
        {
            var element = Instantiate(_heartPrefab, _heartParentPanel);
            element.SetMax();
            _elementList.Add(element);
        }
    }

    private void SetMaxHealth(int maxHealth)
    {
        if (maxHealth > _elementList.Count)
        {
            for (int i = 0; i < maxHealth - _elementList.Count; i++)
            {
                var element = Instantiate(_heartPrefab, _heartParentPanel);
                element.SetMax();
                _elementList.Add(element);
            }
        }
        else if (maxHealth < _elementList.Count)
        {
            for (int i = _elementList.Count - maxHealth; i < _elementList.Count; i++)
            {
                Destroy(_elementList[i].gameObject);
                _elementList.RemoveAt(_elementList.Count - 1);
            }
        }

        SetDamage(playerDamageManager.GetCurrent());
    }

    private void SetDamage(int health)
    {
        if (health <= 0)
        {
            foreach (var element in _elementList)
            {
                element.SetEmpty();
            }
            return;
        }

        fullHeartSpriteAmount = playerDamageManager.GetCurrent() / HEART_SIZE;
        partialHeartSpriteAmount = playerDamageManager.GetCurrent() % HEART_SIZE;
        //Set all full hearts
        for (int i = 0; i < fullHeartSpriteAmount; i++)
        {
            _elementList[i].SetMax();
        }
        //Set partial if exists
        if (partialHeartSpriteAmount > 0)
        {
            _elementList[fullHeartSpriteAmount].SetValue(partialHeartSpriteAmount);
        }
        else
        {
            _elementList[fullHeartSpriteAmount].SetEmpty();
        }

        //Set all empty hearts
        for (int i = fullHeartSpriteAmount + 1; i < _elementList.Count; i++)
        {
            _elementList[i].SetEmpty();
        }
    }


}
