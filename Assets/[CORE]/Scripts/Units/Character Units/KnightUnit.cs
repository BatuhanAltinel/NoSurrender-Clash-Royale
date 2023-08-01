using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUnit : CharacterUnit
{


    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        CheckAttackRange();
    }

    

    public override void AttackToEnemy()
    {
        CheckTargetIsEliminated();

        if (_targetFounded && !_targetEliminated)
        {
            _hitCooldown = 0;

            EventManager.OnCharacterStateChange?.Invoke(this, CharacterState.Attack);

            if (_targetTransform.TryGetComponent(out IDamagable damagable))
            {
                Debug.Log("Knight attacked");
                damagable.TakeDamage(_damage);
            }

            StartCoroutine(WaitAfterAttack());

            
        }
    }

    IEnumerator WaitAfterAttack()
    {
        yield return new WaitForSeconds(_attackSpeed);

        AttackToEnemy();
    }
}
