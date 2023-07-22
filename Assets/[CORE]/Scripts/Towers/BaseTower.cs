using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : Tower
{
    
    void FixedUpdate()
    {
        CheckEnemyUnitInSight();
    }

    private void CheckEnemyUnitInSight()
    {
        _targetUnitColliders = Physics.OverlapSphere(transform.position, _range);

        if (_targetUnitColliders.Length > 0)
        {
            FindEnemyTargets();
        }

    }
}
