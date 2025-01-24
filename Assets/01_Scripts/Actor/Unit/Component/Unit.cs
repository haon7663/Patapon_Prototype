using System;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class Unit : MonoBehaviour
    {
        public StateMachine<Unit> StateMachine { get; private set; }
        
        public Animator Animator { get; private set; }
        
        public Movement Movement { get; private set; }
        
        public Agent Agent { get; private set; }
        
        public BaseAutoAttack AutoAttack { get; private set; }
        
        public GroundChecker GroundChecker { get; private set; }

        public Alliances alliances;

        public bool isDeath;

        private void Awake()
        {
            var visualTransform = transform.Find("Visual");
            Animator = visualTransform.GetComponent<Animator>();
            
            Movement = GetComponent<Movement>();
            Movement?.Init(this);
            
            Agent = GetComponent<Agent>();
            Agent?.Init(this);

            AutoAttack = GetComponent<BaseAutoAttack>();
            AutoAttack?.Init(this);
            
            GroundChecker = GetComponent<GroundChecker>();
            GroundChecker?.Init(this);
                
            StateMachine = new StateMachine<Unit>(this);
            StateMachine.Initialize(UnitStateEnum.Fall);
        }

        private void Update()
        {
            StateMachine.CurrentState.UpdateState();
        }

        public void AnimationTrigger(AnimationTriggerEnum bit)
        {
            StateMachine.CurrentState.AnimationTrigger(bit);
        }

        private void OnEnable()
        {
            UnitManager.RegisterUnit(this);
        }

        private void OnDisable()
        {
            UnitManager.UnregisterUnit(this);
        }
    }
}