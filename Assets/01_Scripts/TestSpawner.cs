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
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)], new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
    }
}
