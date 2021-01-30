using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossMinion
{
    event Action OnDeath;
    void BossIsDead();   
}
