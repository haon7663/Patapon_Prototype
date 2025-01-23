using System;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class Health : MonoBehaviour
    {
        public float maxHp;
        public float curHp;

        private void Start()
        {
            curHp = maxHp = 20;
        }

        public void OnDamage(float damage)
        {
            curHp -= damage;
            if (curHp <= 0)
                GetComponent<Unit>().StateMachine.ChangeState(UnitStateEnum.Death);
        }
    }
}
