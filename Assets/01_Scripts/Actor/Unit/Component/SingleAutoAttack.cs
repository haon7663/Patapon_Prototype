using UnityEngine;

namespace Actor.Unit.Component
{
    public class SingleAutoAttack : BaseAutoAttack
    {
        public override void Strike(Transform target)
        {
            if (target.TryGetComponent(out Health health))
                health.OnDamage(strikingPower);
        }
    }
}
