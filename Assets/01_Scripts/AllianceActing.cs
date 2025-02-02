using System.Linq;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

public static class AllianceActing
{
    public static UnitCommands commands = UnitCommands.Defence;

    public static float GetPositionX(Unit unit)
    {
        var index = UnitManager.Units
            .Where(u => u.alliances == Alliances.Alliance)
            .OrderBy(u => u.AutoAttack.range)
            .ThenByDescending(u => u.transform.position.x)
            .ToList()
            .FindIndex(u => unit == u);

        return -index * 0.3f - unit.AutoAttack.range * 0.25f - 3;
    }
}
