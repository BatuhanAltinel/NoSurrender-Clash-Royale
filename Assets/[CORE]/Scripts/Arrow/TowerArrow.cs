using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrow : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToTarget(Transform target,float hitSpeed)
    {
       // Move to enemy transform
       StartCoroutine(MoveToTargetRoutine(target,hitSpeed));
    } 

    IEnumerator MoveToTargetRoutine(Transform target,float hitSpeed)
    {
        bool _canMove = true;

        while(_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, hitSpeed * Time.deltaTime);
            yield return null;

            if(Vector3.Distance(target.position,transform.position) < 0.2f)
            {
                _canMove = false;
                yield break;
            }
        }
    }

}
