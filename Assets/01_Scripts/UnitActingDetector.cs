using System;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using UnityEngine;

public class UnitActingDetector : MonoBehaviour
{
    [SerializeField] private Vector2 point;
    [SerializeField] private Vector2 size;
    
    private void Update()
    {
        Physics2D.OverlapBox(point, size, 0);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.alliances == Alliances.Enemy)
                AllianceActing.commands = UnitCommands.Attack;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.alliances == Alliances.Enemy)
                AllianceActing.commands = UnitCommands.Defence;
        }
    }
}
