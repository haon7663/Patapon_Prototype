using System;
using System.Linq;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using Actor.Unit.States;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class Agent : MonoBehaviour
    {
        private Action<Vector2> _onMovement;
        public event Action<Vector2> OnMovement
        {
            add
            {
                _onMovement -= value;
                _onMovement += value;
            }
            remove => _onMovement -= value;
        }
        
        private Action _onAttack;
        public event Action OnAttack
        {
            add
            {
                _onAttack -= value;
                _onAttack += value;
            }
            remove => _onAttack -= value;
        }

        private Unit _unit;
        
        public BaseAutoAttack AutoAttack { get; private set; }
        
        public Alliances targetAlliance;
        
        public UnitCommands commands;

        public void Init(Unit unit)
        {
            _unit = unit;
            AutoAttack = GetComponent<BaseAutoAttack>();
        }

        private void LateUpdate()
        {
            switch (commands)
            {
                case UnitCommands.Move:
                    _onMovement?.Invoke(Vector2.right);
                    break;
                case UnitCommands.Attack:
                    var target = SearchTarget();
                    if (target && AutoAttack.InRange && AutoAttack.attackCooldown <= 0)
                    {
                        _onAttack?.Invoke();
                    }
                    else
                    {
                        if (AutoAttack.InRange)
                            _onMovement?.Invoke(Vector2.zero);
                        else
                            _onMovement?.Invoke(target ? target.position.x - transform.position.x > 0 ? Vector2.right : Vector2.left : Vector2.zero);
                    }
                    break;
            }
        }
        
        public Transform SearchTarget()
        {
            return UnitManager.Units
                .Where(u => u.alliances == _unit.Agent.targetAlliance)
                .OrderBy(u => (u.transform.position - transform.position).magnitude).FirstOrDefault()
                ?.transform;
        }
    }

    public enum UnitCommands
    {
        None,
        Move,
        Attack,
        Defence,
    }
}