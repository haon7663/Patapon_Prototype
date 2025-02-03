using System;
using System.Linq;
using Actor.Unit.Enums;
using Actor.Unit.Management;
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

        public Vector2 dir;

        private Unit _mainTarget;

        public void Init(Unit unit)
        {
            _unit = unit;
            AutoAttack = GetComponent<BaseAutoAttack>();
        }

        private void LateUpdate()
        {
            var command = _unit.alliances == Alliances.Alliance ? AllianceActing.commands : commands;
            
            if (command != UnitCommands.Attack)
            {
                var target = SearchTarget();
                if (target && (transform.position - target.position).magnitude < 5)
                {
                    AllianceActing.commands = UnitCommands.Attack;
                }
            }
            else
            {
                var target = SearchTarget();
                if (!target)
                {
                    AllianceActing.commands = UnitCommands.Defence;
                }
            }
            
            switch (command)
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
                        if (_unit.alliances == Alliances.Alliance)
                        {
                            _onMovement?.Invoke(Vector2.zero);
                            dir = Vector2.right;
                            if (dir != Vector2.zero)
                                transform.rotation = Quaternion.Euler(0, dir.x > 0 ? 0 : 180, 0);
                            break;
                        }
                        
                        if (AutoAttack.InRange)
                            _onMovement?.Invoke(Vector2.zero);
                        else
                        {
                            dir = target ? target.position.x - transform.position.x > 0 ? Vector2.right : Vector2.left : Vector2.left;
                            _onMovement?.Invoke(dir);
                        }
                    }
                    break;
                case UnitCommands.Defence:
                    var targetPosX = AllianceActing.GetBoundaryPositionX(_unit);
                    if (Mathf.Abs(targetPosX - transform.position.x) < 0.15f)
                        _onMovement?.Invoke(Vector2.zero);
                    else
                    {
                        dir = targetPosX > transform.position.x ? Vector2.right : Vector2.left;
                        _onMovement?.Invoke(dir);
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