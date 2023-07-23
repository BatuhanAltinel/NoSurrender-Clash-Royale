using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] Pool[] _pools;
    [SerializeField] CharacterPool[] _charPools;

    void Start()
    {
        InitialCreate();
        InitialCharacterCreate();
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

    private void InitialCharacterCreate()
    {
        for (int i = 0; i < _charPools.Length; i++)
        {
            _charPools[i]._pool = new Queue<CharacterUnit>();
            _charPools[i]._charType = _charPools[i]._pooledObject.GetCharacterType();

            for (int j = 0; j < _charPools[i]._amountOfObject; j++)
            {
                CharacterUnit CU = Instantiate(_charPools[i]._pooledObject, Vector3.zero, Quaternion.identity);

                _charPools[i]._pool.Enqueue(CU);
                CU.gameObject.SetActive(false);
                CU.transform.parent = _charPools[i]._objectParent;
            }
        }
    }

    public GameObject GetObjectFromPool(int objectIndex)
    {
        foreach(GameObject obj in _pools[objectIndex]._pool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                _pools[objectIndex]._pool.Dequeue();
                _pools[objectIndex]._pool.Enqueue(obj);
                
                return obj;
            }
        }
        return null;
    }

    public CharacterUnit GetCharacterUnit(CharacterType charType)
    {
        CharacterPool charPool = SetOrderedCharacterPool(charType);

        foreach (CharacterUnit character in charPool._pool)
        {
            if (!character.gameObject.activeInHierarchy)
            {
                //character.gameObject.SetActive(true);
                charPool._pool.Dequeue();
                charPool._pool.Enqueue(character);

                return character;
            }
        }
        return null;

    }


    private CharacterPool SetOrderedCharacterPool(CharacterType charType)
    {
        foreach (CharacterPool charPool in _charPools)
        {
            if(charType == charPool._charType)
            {
                return charPool;
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

    [Serializable]
    class CharacterPool
    {
        public Queue<CharacterUnit> _pool;
        public int _amountOfObject;

        public CharacterUnit _pooledObject;
        public Transform _objectParent;

        public CharacterType _charType;
    }

}
