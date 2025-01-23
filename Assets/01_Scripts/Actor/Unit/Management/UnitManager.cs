using System.Collections.Generic;
using System.Linq;

namespace Actor.Unit.Management
{
    using Component;
    
    public static class UnitManager
    {
        private static List<Unit> _units = new List<Unit>();
        public static List<Unit> Units => _units.Where(u => !u.isDeath).ToList();

        public static void RegisterUnit(Unit unit)
        {
            _units.Add(unit);
        }

        public static void UnregisterUnit(Unit unit)
        {
            if (_units.Contains(unit))
                _units.Remove(unit);
        }
    }
}