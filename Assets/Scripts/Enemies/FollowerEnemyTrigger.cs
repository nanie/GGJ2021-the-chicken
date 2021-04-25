using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyTrigger : MonoBehaviour
{
    FollowerEnemy _FollowerEnemy;
    void Start()
    {
        _FollowerEnemy = GetComponentInParent<FollowerEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _FollowerEnemy.StartFollow(collision.transform);
        }
    }


}
