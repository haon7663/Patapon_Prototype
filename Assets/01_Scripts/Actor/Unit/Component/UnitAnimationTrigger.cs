using UnityEngine;

namespace Actor.Unit.Component
{
    public class UnitAnimationTrigger : MonoBehaviour
    {
        private Unit _unit;
        
        private void Awake()
        {
            _unit = GetComponentInParent<Unit>();
        }
        
        private void AnimationTrigger(AnimationTriggerEnum bit)
        {
            _unit.AnimationTrigger(bit);
        }
    }
}