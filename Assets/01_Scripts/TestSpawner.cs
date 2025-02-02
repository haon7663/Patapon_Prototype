using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Unit = Actor.Unit.Component.Unit;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Unit[] unitPrefabs;
    [SerializeField] private Unit[] enemyUnitPrefabs;

    [SerializeField] private Transform allianceSpawnPoint;
    [SerializeField] private Transform enemySpawnPoint;
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)], allianceSpawnPoint.position, Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(enemyUnitPrefabs[Random.Range(0, enemyUnitPrefabs.Length)], enemySpawnPoint.position, Quaternion.identity);
        }
    }
}
