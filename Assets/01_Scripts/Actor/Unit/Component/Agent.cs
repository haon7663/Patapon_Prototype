using System;
using System.Linq;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

namespace Actor.Unit.Component
{
    public abstract class Agent : MonoBehaviour
    {
        protected Action<Vector2> onMovement;
        public event Action<Vector2> OnMovement
        {
            add
            {
                onMovement -= value;
                onMovement += value;
            }
            remove => onMovement -= value;
        }
        
        protected Action onAttack;
        public event Action OnAttack
        {
            add
            {
                onAttack -= value;
                onAttack += value;
            }
            remove => onAttack -= value;
        }

        protected Unit Unit;
        
        public BaseAutoAttack AutoAttack { get; private set; }
        
        public Alliances targetAlliance;

        public Vector2 dir;

        private Unit _mainTarget;

        private void Awake()
        {
            AutoAttack = GetComponent<BaseAutoAttack>();
        }

        public void Init(Unit unit)
        {
            Unit = unit;
        }
        
        public Transform SearchTarget()
        {
            return UnitManager.Units
                ?.Where(u => u.alliances == targetAlliance)
                .OrderBy(u => (u.transform.position - transform.position).magnitude).FirstOrDefault()
                ?.transform;
        }
    }
}