using Actor.Unit.Component;
using Actor.Unit.Enums;
using UnityEngine;

namespace Actor.Projectile
{
    public class LinearProjectile : Projectile
    {
        private float _damage;
        private Alliances _targetAlliance = Alliances.None;
        
        public override void Init(Transform owner, Transform target, float damage)
        {
            transform.position += Vector3.up * 0.25f;
            GetComponent<Rigidbody2D>().linearVelocity = (target.position - owner.position).normalized * projectileSpeed;
            _damage = damage;
            _targetAlliance = owner.GetComponent<AllianceAgent>().targetAlliance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_targetAlliance == Alliances.None) return;
            if (other.TryGetComponent(out Unit.Component.Unit unit) && unit.alliances == _targetAlliance)
            {
                other.GetComponent<Health>().OnDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}