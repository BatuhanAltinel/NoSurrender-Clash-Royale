using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum CharacterType
{
    Ground = 1,
    Aerial = 2,
    LongRange = 4,
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

    [Header("Character Type")]
    [SerializeField] protected CharacterType _characterType;
    [SerializeField] protected CharacterType _targetCharacterType;

    [Header("Character State")]
    [SerializeField] protected CharacterState _characterState;


}
