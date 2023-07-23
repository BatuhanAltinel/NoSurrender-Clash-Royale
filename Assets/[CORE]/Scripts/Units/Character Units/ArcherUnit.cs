using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUnit : CharacterUnit , IDamagable
{

    [SerializeField] private Transform _arrowThrowPoint;


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

            GameObject arrow = SpawnManager.Instance.SpawnArrow(_arrowThrowPoint, _unitType);

            if (arrow != null && _targetUnit != null)
                arrow.GetComponent<TowerArrow>().AttackToTarget(_targetUnit.transform, _damage);
            _hitCooldown = 0;
        }
    }
}
