using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitFallState : State<Unit>
    {
        public UnitFallState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }
        

        public override void Enter()
        {
            base.Enter();
            Owner.Movement.StopImmediately(true);
            Owner.GroundChecker.OnGroundChecked += HandleLandingEvent;
        }
        
        private void HandleLandingEvent()
        {
            StateMachine.ChangeState(UnitStateEnum.Idle);
        }
        
        public override void Exit()
        {
            base.Exit();
            Owner.Movement.StopImmediately(true);
            Owner.GroundChecker.OnGroundChecked -= HandleLandingEvent;
        }
    }
}
