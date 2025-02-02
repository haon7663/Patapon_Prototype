using System;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class Health : MonoBehaviour
    {
        private Unit _unit;

        public event Action OnDeath;
        
        public float maxHp;
        public float curHp;

        private int _knockBackCount = 1;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void OnDamage(float damage)
        {
            curHp -= damage;
            
            if (curHp <= 0)
            {
                OnDeath?.Invoke();
                GetComponent<Unit>().StateMachine.ChangeState(UnitStateEnum.Death);
                return;
            }
            
            if (_knockBackCount > 0 && curHp / maxHp <= 0.3f)
            {
                _knockBackCount--;
                _unit.StateMachine.ChangeState(UnitStateEnum.KnockBack);
            }
            
            _unit.HpBar.UpdateUI();
        }
    }
}
