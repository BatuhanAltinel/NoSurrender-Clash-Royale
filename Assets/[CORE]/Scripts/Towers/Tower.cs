using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class Tower : Unit
{
   
    
    [Header("Attack Attributes")]


    [SerializeField] protected Transform _arrowThrowPoint;

    protected override void Awake() 
    {
        _targetUnit = null;
    }


    private void Start()
    {
        _maxHitPoints = _hitPoints;
    }

    private void Update()
    {
        //AttackCoolDown();
        CheckAttackRange();
    }




    public override void AttackToEnemy()
    {
        CheckTargetIsEliminated();

        if (_targetFounded && !_targetEliminated)
        {
            _hitCooldown = 0;

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




    //private void FindNearestTarget()
    //{
    //    if (_targetFounded == true) return;

    //    float nearestDistance = float.MaxValue;

    //    foreach (Unit unit in _enemyUnits)
    //    {
    //        float tempDistance = Vector3.Distance(transform.position, unit.transform.position);

    //         if(tempDistance < nearestDistance)
    //         {
    //            nearestDistance = tempDistance;

    //            SetTheTarget(unit);

    //            _targetFounded = true;
    //            _targetEliminated = false;
    //        }
    //    }
    //}










}
