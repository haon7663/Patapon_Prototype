using UnityEngine;

namespace Actor.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float projectileSpeed;
        
        public abstract void Init(Transform owner, Transform target, float damage);
    }
}