using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms;


public enum UnitType
{
    Ally,
    Enemy
}


public abstract class Unit : MonoBehaviour,IDamagable
{
    [Header("Unit Attributes")]

    
    [SerializeField] protected float _hitPoints;
    [SerializeField] protected float _maxHitPoints;
    
    [SerializeField] public UnitType _unitType;
    [SerializeField] protected Slider _healthSlider;


    [Header("Unit Attack Attributes")]

    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _hitCooldown;
    [SerializeField] protected float _sightRange;
    [SerializeField] protected float _damage;

    [Header("Unit Target Attributes")]

    [SerializeField] protected bool _canAttack;
    [SerializeField] protected bool _targetFounded;
    [SerializeField] protected bool _targetEliminated;
    [SerializeField] protected bool _inAttackRange;

    [SerializeField] protected Unit _targetUnit;
    [SerializeField] protected Transform _targetTransform;

    [SerializeField] protected Collider[] _targetUnitColliders;
    [SerializeField] protected LayerMask _unitLayer;


    protected virtual void OnEnable()
    {
        _hitCooldown = _attackSpeed;
        UpdateHealthSlider();
    }

    protected virtual void Awake()
    {
        _maxHitPoints = _hitPoints;
    }

    private void FixedUpdate()
    {
        CheckEnemyUnitInSight();
    }


    public void SetUnitType(UnitType type)
    {
        _unitType = type;
    }


    public float GetUnitHitPoints()
    {
        return _hitPoints;
    }

    public void ResetHitPoints()
    {
        _hitPoints = _maxHitPoints;
    }

    protected void AttackCoolDown()
    {
        _hitCooldown += Time.deltaTime;

        if (_hitCooldown >= _attackSpeed)
        {
            _canAttack = true;
        }
        else
        {
            _canAttack = false;
        }
    }



    protected void CheckEnemyUnitInSight()
    {
        _targetUnitColliders = Physics.OverlapSphere(transform.position, _sightRange, _unitLayer);

        if (_targetUnitColliders.Length > 0)
            FindEnemyTargets();


    }

    protected void FindEnemyTargets()
    {
        if (_targetFounded) return;

        foreach (var col in _targetUnitColliders)
        {

            if (col.gameObject.TryGetComponent(out Unit unit))
            {

                if (unit._unitType != _unitType)
                {
                    SetTheTarget(unit);

                    _targetFounded = true;
                    _targetEliminated = false;

                    break;
                }

            }
        }

    }


    protected void CheckAttackRange()
    {
        if (!_targetFounded) return;

        if (Vector3.Distance(transform.position, _targetUnit.transform.position) <= _attackRange && !_inAttackRange)
        {
            _inAttackRange = true;

            AttackToEnemy();
            
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }




    public void SetTheTarget(Unit unit)
    {
        _targetUnit = unit;
    }

    protected virtual void CheckTargetIsEliminated()
    {
        if (_targetUnit.GetUnitHitPoints() <= 0)
        {
            _targetEliminated = true;
            _targetFounded = false;
            _inAttackRange = false;

            _targetUnit.gameObject.SetActive(false);

            SetTheTarget(null);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _hitPoints -= damageAmount;
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        _healthSlider.value = _hitPoints / _maxHitPoints;
    }

    public abstract void AttackToEnemy();
        

}
