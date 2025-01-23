using System;
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

        private Unit _unit;
        
        public BaseAutoAttack AutoAttack { get; private set; }
        
        public UnitCommands commands;
        public Transform target;

        public void Init(Unit unit)
        {
            _unit = unit;
            AutoAttack = GetComponent<BaseAutoAttack>();
        }

        private void Update()
        {
            switch (commands)
            {
                case UnitCommands.Move:
                    _onMovement?.Invoke(Vector2.right);
                    break;
                case UnitCommands.Attack:
                    if (AutoAttack.InRange) _unit.StateMachine.ChangeState(UnitStateEnum.Attack);
                    else _onMovement?.Invoke(Vector2.right);
                    break;
            }
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