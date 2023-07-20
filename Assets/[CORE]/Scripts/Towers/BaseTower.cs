using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : Tower
{
    
    void FixedUpdate()
    {
        CheckEnemyUnitInSight();
    }
}
