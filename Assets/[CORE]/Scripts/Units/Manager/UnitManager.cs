using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnUnitAttack += OnAttack;
        EventManager.OnCharacterStateChange += OnCharacterStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnCharacterStateChange -= OnCharacterStateChanged;
    }

    void OnAttack(Unit unit)
    {
        unit.AttackToEnemy();
    }

    void OnCharacterStateChanged(CharacterUnit unit,CharacterState state)
    {
        unit.ChangeAnimationByCharacterState(state);
    }
}
