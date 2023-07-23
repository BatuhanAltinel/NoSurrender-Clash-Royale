using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportUnit : CharacterUnit,IDamagable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        _hitPoints -= damageAmount;
        CheckForDie();
    }

    protected override void AttackToEnemy()
    {
        if (_canAttack && _targetFounded && !_targetEliminated)
        {
            if (_targetUnit.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);
            }

            _hitCooldown = 0;
        }
    }
}
