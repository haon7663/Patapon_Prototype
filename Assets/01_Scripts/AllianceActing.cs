using System.Linq;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

public static class AllianceActing
{
    public static float GetReadyPositionX(Unit unit)
    {
        var index = UnitManager.Units
            .Where(u => u.alliances == Alliances.Alliance)
            .OrderBy(u => u.AutoAttack.range)
            .ThenByDescending(u => u.transform.position.x)
            .ToList()
            .FindIndex(u => unit == u);

        var maxCount = UnitManager.Units.Count(u => u.alliances == Alliances.Alliance);

        return -(index - (maxCount - 1) * 0.5f) / maxCount * 3;
    }

    public static float GetBoundaryPositionX(Unit unit)
    {
        var index = UnitManager.Units
            .Where(u => u.alliances == Alliances.Alliance)
            .OrderBy(u => u.AutoAttack.range)
            .ThenByDescending(u => u.transform.position.x)
            .ToList()
            .FindIndex(u => unit == u);

        var maxCount = UnitManager.Units.Count(u => u.alliances == Alliances.Alliance);

        return -(index - (maxCount - 1) * 0.5f) * 0.1f + -unit.AutoAttack.range * 0.5f - 0.5f;
    }
}
