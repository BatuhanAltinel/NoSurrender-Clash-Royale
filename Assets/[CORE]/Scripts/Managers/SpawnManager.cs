using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    ObjectPool _objPool;


    protected override void Awake()
    {
        base.Awake();

        _objPool = GetComponent<ObjectPool>();
    }

    void Start()
    {

    }

    public GameObject SpawnArrow(Transform arrowThrowPoint)
    {
        GameObject go = _objPool.GetObjectFromPool(0);

        if(go != null)
        {
            go.transform.position = arrowThrowPoint.position;
            go.transform.rotation = Quaternion.identity;

            return go;
        }

        return null;
    }

    public CharacterUnit SpawnCharacterUnit(CharacterType charType , Vector3 spawnPoint, UnitType unitType)
    {
        CharacterUnit CU = _objPool.GetCharacterUnit(charType);
        CU.SetUnitType(unitType);

        if (CU != null)
        {
            CU.transform.position = spawnPoint;
            CU.transform.rotation = Quaternion.identity;

            return CU;
        }

        return null;
    }
}
