using System.Collections.Generic;
using UnityEngine;
using System;




public class ObjectPool : MonoBehaviour
{
    [SerializeField] Pool[] _pools;


	void Start()
    {
        InitialCreate();
    }

    private void InitialCreate()
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i]._pool = new Queue<GameObject>();

            for (int j = 0; j < _pools[i]._amountOfObject; j++)
            {
                GameObject go = Instantiate(_pools[i]._pooledObject,Vector3.zero,Quaternion.identity);
                _pools[i]._pool.Enqueue(go);
                go.SetActive(false);
                go.transform.parent = _pools[i]._objectParent;
            }
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach(GameObject obj in _pools[0]._pool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                _pools[0]._pool.Dequeue();
                _pools[0]._pool.Enqueue(obj);
                
                return obj;
            }
        }
        return null;
    }


    [Serializable]
    struct Pool
    {
        public Queue<GameObject> _pool;
        public int _amountOfObject;
        public GameObject _pooledObject;
        public Transform _objectParent;
    }

}
