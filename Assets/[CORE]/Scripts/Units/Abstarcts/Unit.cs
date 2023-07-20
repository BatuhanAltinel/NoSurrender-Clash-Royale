using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum UnitType
{
    Player,
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
    [SerializeField] protected float _sightRange;
    [SerializeField] protected float _damage;
    

    public void SetUnitType(UnitType type)
    {
        _unitType = type;
    }
}
