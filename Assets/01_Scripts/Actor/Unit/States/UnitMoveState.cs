using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitMoveState : State<Unit>
    {
        public UnitMoveState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Owner.Agent.OnMovement += HandleMovementEvent;
        }

        private void HandleMovementEvent(Vector2 dir)
        {
            Owner.Movement.SetVelocity(dir);
            if (dir.magnitude <= 0.1f)
            {
                StateMachine.ChangeState(UnitStateEnum.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Owner.Agent.OnMovement -= HandleMovementEvent;
        }
    }
}
