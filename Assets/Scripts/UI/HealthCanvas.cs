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
    private void OnStart()
    {
        playerDamageManager.OnCharacterDamaged += SetDamage;
        playerDamageManager.OnMaxHealthChange += SetMaxHealth;

        //Calcula a quantidade total de corações necessarios na tela
        var totalHeartQuantity = playerDamageManager.GetCurrent() / HEART_SIZE;

        //Cria pra cada coração um HealthHeartSpriteElement
        for (int i = 0; i < totalHeartQuantity; i++)
        {
            var element = Instantiate(_heartPrefab, _heartParentPanel);
            element.SetMax();
            _elementList.Add(element);
        }
    }

    private void SetMaxHealth(int maxHealth)
    {
        if (maxHealth > (_elementList.Count * HEART_SIZE))
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
            //TODO reescrever de uma forma menos confusa
            for (int i = _elementList.Count - 1; i < maxHealth % HEART_SIZE; i--)
            {
                Destroy(_elementList[i].gameObject);
                _elementList.RemoveAt(i);
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
