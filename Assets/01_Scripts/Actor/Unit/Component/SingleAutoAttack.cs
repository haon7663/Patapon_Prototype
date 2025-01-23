using UnityEngine;

namespace Actor.Unit.Component
{
    public class SingleAutoAttack : BaseAutoAttack
    {
        public override void Strike(Vector2 targetPos)
        {
            Debug.Log($"Strike to {targetPos}");
        }
    }
}
