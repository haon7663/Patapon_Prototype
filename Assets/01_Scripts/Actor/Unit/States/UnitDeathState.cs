using UnityEngine;

namespace Actor.Unit.States
{
    using Component;
    
    public class UnitDeathState : State<Unit>
    {
        public UnitDeathState(Unit owner, StateMachine<Unit> stateMachine, string animBoolName) : base(owner, stateMachine, animBoolName)
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
