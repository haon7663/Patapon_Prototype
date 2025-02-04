using UnityEngine;

namespace Actor.Unit.Component
{
    public class MultipleProjectileAutoAttack : BaseAutoAttack
    {
        [SerializeField] private Projectile.Projectile projectilePrefab;
        [SerializeField] private int projectileCount;
        
        public override void Strike(Transform target)
        {
            var targets = RangeInTargets();
            if (targets == null) return;

            for (int i = 0; i < Mathf.Min(targets.Count, projectileCount); i++)
            {
                var t = targets[i];
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(transform, t, strikingPower);
            }
        }
    }
}
