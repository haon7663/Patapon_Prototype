using Actor.Unit.Component;
using UnityEngine;

namespace Actor.Unit.States
{
    public class UnitMoveState : State<Component.Unit>
    {
        public UnitMoveState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Owner.Agent.OnMovement += HandleMovementEvent;
            Owner.Agent.OnAttack += HandleAttackEvent;
        }

        private void HandleMovementEvent(Vector2 dir)
        {
            Owner.Movement.SetVelocity(dir);
            if (dir.magnitude <= 0.1f)
            {
                StateMachine.ChangeState(UnitStateEnum.Idle);
            }
        }
        
        private void HandleAttackEvent()
        {
            StateMachine.ChangeState(UnitStateEnum.Attack);
        }

        public override void Exit()
        {
            Owner.Agent.OnMovement -= HandleMovementEvent;
            Owner.Agent.OnAttack -= HandleAttackEvent;
            base.Exit();
        }
    }
}
