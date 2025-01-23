using UnityEngine;

namespace Actor.Unit.Component
{
    using Projectile;
    
    public class ProjectileAutoAttack : BaseAutoAttack
    {
        [SerializeField] private Projectile projectilePrefab;
        
        public override void Strike(Transform target)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(transform, target, strikingPower);
        }
    }
}
