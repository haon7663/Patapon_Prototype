using System.Collections.Generic;
using System.Linq;

namespace Actor.Unit.Management
{
    public static class UnitManager
    {
        private static List<Component.Unit> _units = new List<Component.Unit>();
        public static List<Component.Unit> Units => _units.Where(u => !u.isDeath).ToList();

        public static void RegisterUnit(Component.Unit unit)
        {
            _units.Add(unit);
        }

        public static void UnregisterUnit(Component.Unit unit)
        {
            if (_units.Contains(unit))
                _units.Remove(unit);
        }
    }
}