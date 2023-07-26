using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private float _randomX_Offset;

    CharacterType charType;
    

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 5f);
    }

    private void SpawnEnemy()
    {
        if (!GameManager.Instance.IsGameState(GameState.InGame)) return;

        float randomNum = Random.Range(0, 2);

        if (randomNum % 2 == 0) charType = CharacterType.Knight;
        else charType = CharacterType.Archer;

        CharacterUnit CU = SpawnManager.Instance.SpawnCharacterUnit(charType,GetRandomPosition(),UnitType.Enemy);
        CU.ResetHitPoints();
        CU.gameObject.SetActive(true);

        //CU.SetTargetTower(CalculateNearestTower(CU));
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-_randomX_Offset, _randomX_Offset);

        Vector3 newPos = new Vector3(randomX, _enemySpawnPoint.position.y, _enemySpawnPoint.position.z);

        return newPos;
    }
}
