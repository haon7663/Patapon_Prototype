using System;
using System.Linq;
using Actor.Unit.Management;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField]
        protected float range;

        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public bool InRange => UnitManager.Units.Any(u => u.alliances == _unit.Agent.targetAlliance && (u.transform.position - transform.position).magnitude <= range);

        public Transform SearchTarget()
        {
            return UnitManager.Units
                .Where(u => u.alliances == _unit.Agent.targetAlliance && (u.transform.position - transform.position).magnitude <= range)
                .OrderBy(u => (u.transform.position - transform.position).magnitude).FirstOrDefault()
                ?.transform;
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
