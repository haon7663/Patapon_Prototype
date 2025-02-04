using System.Linq;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using UnityEngine;

namespace Actor.Projectile
{
    public class FireRudyProjectile : Projectile
    {
        private float _damage;
        private Alliances _targetAlliance = Alliances.None;
        
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private float impactRadius;
        
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

            if (!other.TryGetComponent(out Unit.Component.Unit unit) || unit.alliances != _targetAlliance) return;
            
            var rangeInUnit =
                UnitManager.Units.Where(u => u.alliances == _targetAlliance && (u.transform.position - transform.position).magnitude < impactRadius).ToList();
            
            if (!rangeInUnit.Any()) return;
            
            Instantiate(impactEffect, transform.position, Quaternion.identity);
                
            for (var i = rangeInUnit.Count - 1; i >= 0; i--)
                rangeInUnit[i].GetComponent<Health>().OnDamage(_damage);
                
            Destroy(gameObject);
        }
    }
}
