using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitIdleState : State<Unit>
    {
        public UnitIdleState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Owner.Movement.StopImmediately();
            Owner.Agent.OnMovement += HandleMovementEvent;
        }

        private void HandleMovementEvent(Vector2 dir)
        {
            if (dir.magnitude > 0.1f)
            {
                StateMachine.ChangeState(UnitStateEnum.Move);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            Owner.Agent.OnMovement -= HandleMovementEvent;
        }
    }
}