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

    public TowerArrow SpawnArrow(Transform arrowThrowPoint)
    {
        GameObject go = _objPool.GetObjectFromPool(0);

        if(go != null)
        {
            go.transform.position = arrowThrowPoint.position;
            go.transform.rotation = Quaternion.identity;

            TowerArrow arrow = go.GetComponent<TowerArrow>(); 
            return arrow;
        }

        return null;
    }
}
