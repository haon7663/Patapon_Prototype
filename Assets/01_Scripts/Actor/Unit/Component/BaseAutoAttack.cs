using UnityEngine;

namespace Actor.Unit.Component
{
    public abstract class BaseAutoAttack : MonoBehaviour
    {
        [SerializeField]
        protected float strikingPower;
        [SerializeField]
        protected float range;
        [SerializeField]
        protected LayerMask targetLayer;

        public bool InRange => Physics2D.OverlapCircle(transform.position, range, targetLayer);

        public abstract void Strike(Vector2 targetPos);
    }
}
