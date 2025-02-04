using System.Collections.Generic;
using System.Linq;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

namespace Actor.Unit.Component
{
    public abstract class BaseAutoAttack : MonoBehaviour
    {
        [SerializeField]
        protected float attackDelay;
        [HideInInspector]
        public float attackCooldown;
        
        [SerializeField]
        protected float strikingPower;
        
        public float range;

        protected Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public bool InNearRange => UnitManager.Units.Any(u => u.alliances == _unit.Agent.targetAlliance && Mathf.Abs(u.transform.position.x - transform.position.x) <= range + 1);
        
        public bool InRange => UnitManager.Units.Any(u => u.alliances == _unit.Agent.targetAlliance && Mathf.Abs(u.transform.position.x - transform.position.x) <= range);

        public Transform SearchTarget()
        {
            if (_unit.alliances == Alliances.Enemy)
            {
                if (!UnitManager.Units
                        .Where(u => u.alliances == _unit.Agent.targetAlliance &&
                                    Mathf.Abs(u.transform.position.x - transform.position.x) <= range)
                        .OrderBy(u => Mathf.Abs(u.transform.position.x - transform.position.x)).FirstOrDefault())
                    return transform.Find("Tower");
            }
            
            return UnitManager.Units
                .Where(u => u.alliances == _unit.Agent.targetAlliance && Mathf.Abs(u.transform.position.x - transform.position.x) <= range)
                .OrderBy(u => Mathf.Abs(u.transform.position.x - transform.position.x)).FirstOrDefault()
                ?.transform;
        }

        public List<Transform> RangeInTargets()
        {
            return UnitManager.Units
                .Where(u => u.alliances == _unit.Agent.targetAlliance &&
                            Mathf.Abs(u.transform.position.x - transform.position.x) <= range)
                .Select(u => u.transform).ToList();
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;
        }

        public void TryAttack(Transform target)
        {
            if (attackCooldown <= 0)
            {
                attackCooldown = attackDelay;
                Strike(target);
            }
        }

        public abstract void Strike(Transform target);
    }
}
