using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTower : Tower
{

    void FixedUpdate()
    {
        CheckEnemyUnitInSight();
    }
}
