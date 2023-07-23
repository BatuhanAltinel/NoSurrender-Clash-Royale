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


public abstract class Unit : MonoBehaviour
{
    [Header("Unit Attributes")]

    [SerializeField] protected int _mana;
    [SerializeField] protected float _hitPoints;
    [SerializeField] protected int _unitAmount;
    [SerializeField] public UnitType _unitType;


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

    [SerializeField] protected Unit _targetUnit;
    [SerializeField] protected Collider[] _targetUnitColliders;
    [SerializeField] protected LayerMask _unitLayer;

    private void Awake()
    {
        _hitCooldown = _attackSpeed;
    }

    private void FixedUpdate()
    {
        CheckEnemyUnitInSight();
    }


    public void SetUnitType(UnitType type)
    {
        _unitType = type;
    }



    protected void CheckForDie()
    {
        if (_hitPoints <= 0)
            gameObject.SetActive(false);
    }


    public float GetUnitHitPoints()
    {
        return _hitPoints;
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
        _targetUnitColliders = Physics.OverlapSphere(transform.position, _attackRange, _unitLayer);

        if (_targetUnitColliders.Length > 0)
            FindEnemyTargets();


    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }


    protected void FindEnemyTargets()
    {
        if (_targetFounded) return;

        foreach (var col in _targetUnitColliders)
        {

            if (col.gameObject.TryGetComponent(out CharacterUnit unit) && !_targetFounded)
            {



                if (unit._unitType != _unitType)
                {
                    Debug.Log("Unit bulundu" + col.name);

                    SetTheTarget(unit);

                    _targetFounded = true;
                    _targetEliminated = false;

                    //AttackToEnemy();

                    break;
                }

            }
        }

    }


    void SetTheTarget(Unit unit)
    {
        _targetUnit = unit;
    }

    protected abstract void AttackToEnemy();
        

}
