using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [Header("Attack Attributes")]

    [SerializeField] protected float _range;

    [SerializeField] protected TowerArrow _towerArrow;
    [SerializeField] protected Transform _arrowThrowPoint;

    [SerializeField] protected float _hitSpeed;
    [SerializeField] protected float _damage;
    




    private void Awake() 
    {
        _currentPosition = transform.position;
        _currentScale = transform.localScale;
        _sphereCol = GetComponent<SphereCollider>();    
    }


    public virtual void Start()
    {
        CalculateColliderSizeDependsOnRange();

    }

    private void CalculateColliderSizeDependsOnRange()
    {
        float centerYPos = -(_currentPosition.y / _currentScale.y);
        _sphereCol.center = new Vector3(0, centerYPos, 0);
        _sphereCol.radius = _range /2;
    }

    private void SetTheTarget(EnemyUnit target)
    {
        _targetTransform = target.transform;
    }


    private void AttackToEnemy()
    {
        _towerArrow.MoveToTarget(_targetTransform);
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if(!_targetSelected)
        {
            if(other.TryGetComponent<EnemyUnit>(out EnemyUnit enemyUnit))
            {
                _targetSelected = true;

                SetTheTarget(enemyUnit);
            }
        }
    }
}
