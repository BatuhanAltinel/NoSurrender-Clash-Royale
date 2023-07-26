using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUnit : CharacterUnit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        RotateToTarget();
        CheckAttackRange();
    }

    

    public override void AttackToEnemy()
    {
        CheckTargetIsEliminated();

        if (_targetFounded && !_targetEliminated)
        {
            _hitCooldown = 0;

            EventManager.OnCharacterStateChange?.Invoke(this, CharacterState.Attack);

            if (_targetUnit.TryGetComponent(out IDamagable damagable))
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
