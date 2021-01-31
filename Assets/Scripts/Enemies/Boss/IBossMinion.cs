using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossMinion
{
    event Action<GameObject> OnDeath;
    void BossIsDead();   
}
