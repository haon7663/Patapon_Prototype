using System;
using System.Linq;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

public class UnitGroupFollow : MonoBehaviour
{
    [SerializeField] private Alliances targetAlliances;
    [SerializeField] private Vector2 baseOffset;

    private void Update()
    {
        var targetUnits = UnitManager.Units.Where(u => u.alliances == targetAlliances).ToList();
        if (!targetUnits.Any()) return;
        
        var sumX = targetUnits.Sum(unit => unit.transform.position.x);
        sumX /= targetUnits.Count;
        
        transform.position = baseOffset + Vector2.right * sumX;
    }
}
