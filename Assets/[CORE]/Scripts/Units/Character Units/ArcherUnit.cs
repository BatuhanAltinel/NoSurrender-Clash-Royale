using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUnit : CharacterUnit
{

    [SerializeField] private Transform _arrowThrowPoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        RotateToTarget();
        CheckAttackRange();
    }

    public override void AttackToEnemy()
    {
        CheckTargetIsEliminated();

        if (_targetFounded && !_targetEliminated)
        {
            _hitCooldown = 0;

            EventManager.OnCharacterStateChange?.Invoke(this, CharacterState.Attack);

            GameObject arrow = SpawnManager.Instance.SpawnArrow(_arrowThrowPoint);

            arrow.GetComponent<TowerArrow>().AttackToTarget(_targetUnit.transform, _damage);

            StartCoroutine(WaitAfterAttack());

            
        }
    }

    IEnumerator WaitAfterAttack()
    {
        yield return new WaitForSeconds(_attackSpeed);

        AttackToEnemy();
    }
}
