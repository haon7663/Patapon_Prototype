using UnityEngine;

namespace Actor.Unit.Component
{
    public class Unit : MonoBehaviour
    {
        public StateMachine<Unit> StateMachine { get; private set; }
        
        public Movement Movement { get; private set; }
        
        public Agent Agent { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<Movement>();
            Movement.Init(this);
            
            Agent = GetComponent<Agent>();
            Agent.Init(this);
                
            StateMachine = new StateMachine<Unit>(this);
            StateMachine.Initialize(UnitStateEnum.Idle);
        }
    }
}