using Actor.Unit.Component;
using UnityEngine;
using DG.Tweening;

namespace Actor.Unit.States
{
    public class UnitFallState : State<Component.Unit>
    {
        public UnitFallState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }
        

        public override void Enter()
        {
            base.Enter();
            Owner.Movement.StopImmediately(true);

            var randY = Random.Range(-0.4f, 0.4f);
            Owner.transform.DOLocalMoveY(Owner.transform.localPosition.y + randY, 1.25f)
                .From(Owner.transform.localPosition.y + randY + 8f)
                .SetEase(Ease.InSine)
                .OnComplete(HandleLandingEvent);
        }
        
        private void HandleLandingEvent()
        {
            StateMachine.ChangeState(UnitStateEnum.Idle);
        }
        
        public override void Exit()
        {
            base.Exit();
            Owner.Movement.StopImmediately(true);
        }
    }
}
