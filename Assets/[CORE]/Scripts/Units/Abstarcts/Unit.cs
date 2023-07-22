using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


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

    [SerializeField] protected bool _canAttack;

    private void Awake()
    {
        _hitCooldown = _attackSpeed;
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

}
