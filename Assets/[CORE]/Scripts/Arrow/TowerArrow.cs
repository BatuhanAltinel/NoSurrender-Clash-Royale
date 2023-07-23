using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrow : MonoBehaviour
{
    [SerializeField] float _arrowSpeed;
    [SerializeField] UnitType _unitType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetUnitType(UnitType type)
    {
        _unitType = type;
    }


    public void AttackToTarget(Transform target,float damage)
    {
       // Move to enemy transform
       StartCoroutine(MoveToTargetRoutine(target,damage));
    } 

    IEnumerator MoveToTargetRoutine(Transform target, float damage)
    {
        bool _canMove = true;

        while(_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _arrowSpeed * Time.deltaTime);
            yield return null;

            if(Vector3.Distance(target.position,transform.position) < 0.1f)
            {
                if (target.TryGetComponent<Unit>(out Unit unit))
                {

                    if (unit._unitType != _unitType)
                    {
                        IDamagable dmg = unit.GetComponent<IDamagable>();

                        

                        dmg.TakeDamage(damage);
                        
                        gameObject.SetActive(false);

                        Debug.Log("Unit took damage");
                        //EventManager.OnTowerAttack?.Invoke(unit);
                    }

                }
                _canMove = false;
                yield break;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<Unit>(out Unit unit))
    //    {
            
    //        if (unit._unitType == UnitType.Enemy)
    //        {
    //            IDamagable dmg = unit.GetComponent<IDamagable>();

    //            dmg.TakeDamage(15); // for test

    //            gameObject.SetActive(false);

    //            Debug.Log("Unit took damage");
    //        }

    //    }
    //}

}
