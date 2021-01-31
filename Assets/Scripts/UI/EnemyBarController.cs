using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBarPrefab;
    public void CreateBar(DamageManager manager, Transform target)
    {
        var newBar = Instantiate(healthBarPrefab);
        newBar.StartBar(manager, target);
    }
}
