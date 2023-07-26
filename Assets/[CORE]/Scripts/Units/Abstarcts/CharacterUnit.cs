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
    protected Animator _anim;
    [SerializeField] GameObject _innerModel;

    [SerializeField] protected int _mana;
    [SerializeField] protected int _unitAmount;

    [Header("Movement Attributes")]
    [SerializeField] protected float _moveSpeed;

    [Header("Target Attributes")]
    [SerializeField] protected Tower _targetTower;
    

    [Header("Character Type")]
    [SerializeField] protected CharacterType _characterType;

    [Header("Character State")]
    [SerializeField] protected CharacterState _characterState;

    protected override void OnEnable()
    {
        base.OnEnable();

        CalculateNearestEnemyTower();
        ChangeCharacterState(CharacterState.Walk);
    }

    protected override void Awake()
    {
        base.Awake();
        _anim = _innerModel.GetComponent<Animator>();
        
    }

    protected void MoveToTarget()
    {
        if (_characterState == CharacterState.Attack) return;

        //if(_targetTower != null && !_targetFounded)
        //    transform.position = Vector3.MoveTowards(transform.position, _targetTower.transform.position, _moveSpeed * Time.deltaTime);

        //if(_targetFounded && !_targetEliminated)
        //    transform.position = Vector3.MoveTowards(transform.position, _targetUnit.transform.position, _moveSpeed * Time.deltaTime);

        if (_targetTower != null && !_targetFounded) _targetTransform = _targetTower.transform;
        else if (_targetFounded && !_targetEliminated) _targetTransform = _targetUnit.transform;

        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _moveSpeed * Time.deltaTime);
    }

    protected void RotateToTarget()
    {
        _innerModel.transform.rotation = Quaternion.Lerp(transform.rotation, _targetTransform.rotation, 0.2f);
    }

    public void SetTargetTower(Tower tower)
    {
        _targetTower = tower;
    }

    public CharacterType GetCharacterType()
    {
        return _characterType;
    }

    protected void ChangeCharacterState(CharacterState state)
    {
        _characterState = state;
    }

    public void ChangeAnimationByCharacterState(CharacterState state)
    {
        _characterState = state;

        switch (_characterState)
        {
            case CharacterState.Walk:
                _anim.SetBool("OnAttack", false);
                break;
            case CharacterState.Attack:
                _anim.SetBool("OnAttack", true);
                break;
            default:
                break;
        }
    }

    protected override void CheckTargetIsEliminated()
    {
        base.CheckTargetIsEliminated();

        if(_targetEliminated)
            EventManager.OnCharacterStateChange?.Invoke(this, CharacterState.Walk);
        //ChangeCharacterState(CharacterState.Walk);
    }

    protected void CalculateNearestEnemyTower()
    {
        float nearestDistance = float.MaxValue;
        Tower _nearestTower = null;

        foreach (Tower tower in TowerManager.Instance.GetAllTowers())
        {
            if (tower._unitType == _unitType || !tower.gameObject.activeInHierarchy)
                continue;

            float tempDistance = Vector3.Distance(transform.position, tower.transform.position);

            if (tempDistance < nearestDistance)
            {
                nearestDistance = tempDistance;

                _nearestTower = tower;
            }
        }

        SetTargetTower(_nearestTower);
    }



}
