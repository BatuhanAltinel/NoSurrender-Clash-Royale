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


public abstract class CharacterUnit : Unit
{

    [Header("Movement Attributes")]
    [SerializeField] protected float _moveSpeed;

    [Header("Character Type")]
    [SerializeField] protected CharacterType _characterType;
    [SerializeField] protected CharacterType _targetCharacterType;


}
