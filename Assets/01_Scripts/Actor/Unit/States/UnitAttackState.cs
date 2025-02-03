using Actor.Unit.Component;

namespace Actor.Unit.States
{
    public class UnitAttackState : State<Component.Unit>
    {
        public UnitAttackState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
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
