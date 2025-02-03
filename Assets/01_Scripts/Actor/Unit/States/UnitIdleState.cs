using Actor.Unit.Component;
using UnityEngine;

namespace Actor.Unit.States
{
    public class UnitIdleState : State<Component.Unit>
    {
        public UnitIdleState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Owner.Movement.StopImmediately(true);
            Owner.Agent.OnMovement += HandleMovementEvent;
            Owner.Agent.OnAttack += HandleAttackEvent;
        }

        private void HandleMovementEvent(Vector2 dir)
        {
            if (dir.magnitude > 0.1f)
            {
                StateMachine.ChangeState(UnitStateEnum.Move);
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