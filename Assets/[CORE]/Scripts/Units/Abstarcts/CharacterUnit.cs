using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterType
{
    Knight = 1,
    Archer = 2,
    Dragon = 4,
    Support = 8
}

public enum CharacterState
{
    Walk,
    Attack
}


public abstract class CharacterUnit : Unit
{

    [Header("Movement Attributes")]
    [SerializeField] protected float _moveSpeed;

    [Header("Target Attributes")]
    [SerializeField] protected Tower _mainTower; 
    [SerializeField] protected Tower[] _sideTowers;

    [SerializeField] protected Tower _targetTower;

    [Header("Character Type")]
    [SerializeField] protected CharacterType _characterType;
    [SerializeField] protected CharacterType _targetCharacterType;

    [Header("Character State")]
    [SerializeField] protected CharacterState _characterState;


    //protected void MoveToTarget()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position,_mainTower.transform.position, _moveSpeed * Time.deltaTime);
    //}

    protected void MoveToTarget()
    {
        if(_targetTower != null)
            transform.position = Vector3.MoveTowards(transform.position, _targetTower.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetTower(Tower tower)
    {
        _targetTower = tower;
    }

    public CharacterType GetCharacterType()
    {
        return _characterType;
    }


}
