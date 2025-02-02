using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitKnockBackState : State<Unit>
    {
        public UnitKnockBackState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Owner.Movement.AddForceImpulse(-Owner.Agent.dir * 4);
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            
            if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
            {
                StateMachine.ChangeState(UnitStateEnum.Idle);
                Debug.Log("KnockBackEnd");
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            Owner.Movement.StopImmediately(true);
        }
    }
}
