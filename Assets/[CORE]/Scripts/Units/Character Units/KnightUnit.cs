using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUnit : CharacterUnit, IDamagable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        CheckForDie();
    }

    public void TakeDamage(float damageAmount)
    {
        _hitPoints -= damageAmount;
    }
}
