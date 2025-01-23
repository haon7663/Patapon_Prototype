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
            base.Enter();
            Owner.Movement.StopImmediately();
            Debug.Log("Unit is attacking");
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}
