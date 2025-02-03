using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actor.Unit.Component
{
    public class Unit : MonoBehaviour
    {
        public StateMachine<Unit> StateMachine { get; private set; }
        
        public Animator Animator { get; private set; }
        
        public Movement Movement { get; private set; }
        
        public Agent Agent { get; private set; }
        
        public Health HealthComp { get; private set; }
        
        public BaseAutoAttack AutoAttack { get; private set; }
        
        public GroundChecker GroundChecker { get; private set; }

        public Alliances alliances;

        public bool isDeath;

        public HpBar HpBar { get; private set; }
        
        public HpBar hpBarPrefab;
        
        public Transform NormalTrans { get; private set; }
        
        public Transform VisualTrans { get; private set; }
        
        public SpriteRenderer SpriteRendererComp { get; private set; }

        private void Awake()
        {
            NormalTrans = transform.Find("Normal");
            
            VisualTrans = NormalTrans.Find("Visual");
            Animator = VisualTrans.GetComponent<Animator>();
            SpriteRendererComp = VisualTrans.GetComponent<SpriteRenderer>();
            
            Movement = GetComponent<Movement>();
            Movement?.Init(this);
            
            Agent = GetComponent<Agent>();
            Agent?.Init(this);

            HealthComp = GetComponent<Health>();
            HealthComp?.Init(this);

            AutoAttack = GetComponent<BaseAutoAttack>();
            AutoAttack?.Init(this);
            
            GroundChecker = GetComponent<GroundChecker>();
            GroundChecker?.Init(this);
                
            StateMachine = new StateMachine<Unit>(this);
            StateMachine.Initialize(UnitStateEnum.Fall);

            var canvas = FindAnyObjectByType<Canvas>();
            HpBar = Instantiate(hpBarPrefab, canvas.transform);
            HpBar.Connect(this);
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