using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : BaseEnemy
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetDamage(collision.gameObject);
    }
}
