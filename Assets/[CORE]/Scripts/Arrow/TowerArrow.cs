using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrow : MonoBehaviour
{
    [SerializeField] float _arrowSpeed;
    bool _damageApplied;


    public void AttackToTarget(Transform target,float damage)
    {
       StartCoroutine(MoveToTargetRoutine(target,damage));
    } 

    IEnumerator MoveToTargetRoutine(Transform target, float damage)
    {
        bool _canMove = true;
        _damageApplied = false;

        while(_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _arrowSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(target.position);

            if(Vector3.Distance(target.position,transform.position) < 0.01f)
            {
                gameObject.SetActive(false);
                _canMove = false;

                if (target.TryGetComponent(out IDamagable damagable) && !_damageApplied)
                {
                    damagable.TakeDamage(damage);

                    _damageApplied = true;
                }

                yield break;
            }

            yield return null;
        }
    }

}
