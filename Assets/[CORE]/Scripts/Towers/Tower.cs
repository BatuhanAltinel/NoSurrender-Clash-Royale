using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tower : MonoBehaviour
{
    [Header("Tower Attributes")]
    
    [SerializeField] protected float _hitPoints;
    private Vector3 _currentScale;
    private Vector3 _currentPosition;
    SphereCollider _sphereCol;

    
    [Header("Target Attributes")]

    [SerializeField] protected Transform _targetTransform;
    [SerializeField] protected bool _targetSelected;
    [SerializeField] protected Collider[] _targetUnitColliders; 
    
    [Header("Attack Attributes")]

    [SerializeField] protected float _range;

    [SerializeField] protected TowerArrow _towerArrow;
    [SerializeField] protected Transform _arrowThrowPoint;

    [SerializeField] protected float _hitSpeed;
    private protected float _hitCooldown;
    private protected bool _canAttack;
    [SerializeField] protected float _damage;
    




    private void Awake() 
    {
        _currentPosition = transform.position;
        _currentScale = transform.localScale;
        _sphereCol = GetComponent<SphereCollider>();    
    }


    private void Start()
    {
        _hitCooldown = _hitSpeed;
    }

    void Update()
    {
        AttackCoolDown();
    }
    
    protected void CheckEnemyUnitInSight()
    {
        _targetUnitColliders =  Physics.OverlapSphere(transform.position, _range);

        foreach (Collider col in _targetUnitColliders)
        {
            if(col.TryGetComponent<Unit>(out Unit unit) && !_targetSelected)
            {
                Debug.Log("Unit founded");
               if(unit._unitType == UnitType.Enemy)
               {
                    _targetSelected = true;
                    SetTheTarget(col.GetComponent<Unit>());
                    AttackToEnemy(_hitSpeed);
                    Debug.Log("target detected");
                    break;
               }
 
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void SetTheTarget(Unit target)
    {
        _targetTransform = target.transform;
    }


    private void AttackToEnemy(float hitSpeed)
    {
        if(_canAttack)
        {
            TowerArrow arrow = SpawnManager.Instance.SpawnArrow(_arrowThrowPoint);
            arrow.MoveToTarget(_targetTransform,hitSpeed);
            
        }
            
    }

    private void AttackCoolDown()
    {
        _hitCooldown += Time.deltaTime;

        if(_hitCooldown >= _hitSpeed)
        {
            _canAttack = true;
        }else
        {
            _canAttack = false;
            _hitCooldown = 0;
        }
    }


}
