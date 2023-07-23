using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private float _randomX_Offset;

    [SerializeField] private Tower[] _towers;
    [SerializeField] private Tower _nearestTower;

    CharacterType charType;
    

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 5f);
    }

    private void SpawnEnemy()
    {
        float randomNum = Random.Range(0, 2);

        if (randomNum % 2 == 0) charType = CharacterType.Knight;
        else charType = CharacterType.Archer;

        CharacterUnit CU = SpawnManager.Instance.SpawnCharacterUnit(charType,GetRandomPosition(),UnitType.Enemy);
        CU.gameObject.SetActive(true);

        CU.SetTargetTower(CalculateNearestTower(CU));
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-_randomX_Offset, _randomX_Offset);

        Vector3 newPos = new Vector3(randomX, _enemySpawnPoint.position.y, _enemySpawnPoint.position.z);

        return newPos;
    }

    private Tower CalculateNearestTower(Unit unit)
    {
        float nearestDistance = float.MaxValue;

        foreach (Tower tower in _towers)
        {
            float tempDistance = Vector3.Distance(unit.transform.position, tower.transform.position);

            if (tempDistance < nearestDistance)
            {
                nearestDistance = tempDistance;

                _nearestTower = tower;
            }
        }

        return _nearestTower;
    }
}
