using System;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class Health : MonoBehaviour
    {
        public float maxHp;
        public float curHp;

        public void OnDamage(float damage)
        {
            curHp -= damage;
            if (curHp <= 0)
                GetComponent<Unit>().StateMachine.ChangeState(UnitStateEnum.Death);
        }
    }
}
