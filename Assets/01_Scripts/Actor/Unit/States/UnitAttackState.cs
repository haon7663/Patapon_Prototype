using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitAttackState : State<Unit>
    {
        public UnitAttackState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            RemoveTrigger(AnimationTriggerEnum.EndTrigger);
            base.Enter();
            Owner.Movement.StopImmediately(true);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (IsTriggerCalled(AnimationTriggerEnum.AttackTrigger))
            {
                var target = Owner.AutoAttack.SearchTarget();
                if (target)
                {
                    Owner.AutoAttack.TryAttack(target);
                }
                RemoveTrigger(AnimationTriggerEnum.AttackTrigger);
            }

            if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
            {
                StateMachine.ChangeState(UnitStateEnum.Move);
            }
        }
    }
}
