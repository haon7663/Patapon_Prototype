using Actor.Unit.Component;
using UnityEngine;

namespace Actor.Unit.States
{
    public class UnitKnockBackState : State<Component.Unit>
    {
        public UnitKnockBackState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
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
                RemoveTrigger(AnimationTriggerEnum.EndTrigger);
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
