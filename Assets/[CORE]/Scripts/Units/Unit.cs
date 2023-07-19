using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Unit Attributes")]

    [SerializeField] protected int _mana;
    [SerializeField] protected float _hitPoints;
    [SerializeField] protected int _unitAmount;

    [Header("Unit Attack Attributes")]

    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _sightRange;
    [SerializeField] protected float _damage;
    
}
