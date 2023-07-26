using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    [SerializeField] Tower[] _tower;
    
    public Tower[] GetAllTowers()
    {
        return _tower;
    }
}
