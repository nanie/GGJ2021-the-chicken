using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barFiller;
    private DamageManager manager;
    private int total;
    Transform target;
    float speed = 5;
    public void StartBar(DamageManager manager, Transform target)
    {
        this.manager = manager;
        this.target = target;
        Vector3 targetPosition = target.position;
        targetPosition.z = 0;
        transform.position = targetPosition;
        manager.OnCharacterDamaged += ChangeValues;
        manager.OnCharacterDie += Died;
        total = manager.GetCurrent();
    }

    private void Died()
    {
        manager.OnCharacterDie -= Died;
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (target != null)
            FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.OnCharacterDamaged -= ChangeValues;
        }
    }
    private void ChangeValues(int value)
    {
        barFiller.fillAmount = (float)value / (float)total;
      
    }
}
