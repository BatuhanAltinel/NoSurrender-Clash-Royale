using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrow : MonoBehaviour
{
    [SerializeField] float _arrowSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToTarget(Transform target)
    {
       // Move to enemy transform
       StartCoroutine(MoveToTargetRoutine(target));
    } 

    IEnumerator MoveToTargetRoutine(Transform target)
    {
        bool _canMove = true;

        while(_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _arrowSpeed * Time.deltaTime);
            yield return null;

            if(Vector3.Distance(target.position,transform.position) < 0.2f)
            {
                _canMove = false;
                yield break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Unit>(out Unit unit))
        {
            
            if (unit._unitType == UnitType.Enemy)
            {
                IDamagable dmg = unit.GetComponent<IDamagable>();

                dmg.TakeDamage(15); // for test

                gameObject.SetActive(false);

                Debug.Log("Unit took damage");
            }

        }
    }

}
