using UnityEngine;

namespace Actor.Unit.States
{
    public class UnitDeathState : State<Component.Unit>
    {
        public UnitDeathState(Component.Unit owner, StateMachine<Component.Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Owner.Movement.StopImmediately(true);
            Owner.GetComponent<BoxCollider2D>().enabled = false;
            Owner.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Owner.isDeath = true;
        }
        
        public override void UpdateState()
        {
            base.UpdateState();

            if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
            {
                Object.Destroy(Owner.gameObject);
            }
        }
    }
}
