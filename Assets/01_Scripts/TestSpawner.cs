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

    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private Transform canvas;
    
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
            for (int i = 0; i < 5; i++)
                Instantiate(enemyUnitPrefabs[Random.Range(0, enemyUnitPrefabs.Length)], enemySpawnPoint.position + Vector3.right * Random.Range(-1f, 4f), Quaternion.identity);
            
            Instantiate(warningPrefab, canvas);
        }
    }
}
