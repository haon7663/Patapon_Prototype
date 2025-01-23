using System;
using System.Collections;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using UnityEngine;

namespace Actor.Projectile
{
    public class ParabolaProjectile : Projectile
    {
        private float _damage;
        private Alliances _targetAlliance = Alliances.None;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        public float firingAngle = 45.0f;
        public float gravity = 9.8f;
        
        public override void Init(Transform owner, Transform target, float damage)
        {
            _startPosition = transform.position + Vector3.up * 0.25f;
            _endPosition = target.position + Vector3.up * 0.25f;
            
            _damage = damage;
            _targetAlliance = owner.GetComponent<Agent>().targetAlliance;

            StartCoroutine(Parabola());
        }

        public IEnumerator Parabola()
        {
            float distance = Vector3.Distance(_startPosition, _endPosition);
            
            float velocity = distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
            
            float Vx = Mathf.Sqrt(velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
            
            float flightDuration = distance / Vx;
            
            //transform.rotation = Quaternion.LookRotation(_endPosition - _startPosition);
            
            float elapseTime = 0;
            while (elapseTime < flightDuration)
            {
                transform.Translate(Vx * Time.deltaTime, ((Vy - (gravity * elapseTime)) * Time.deltaTime) / distance * 4, 0);
                elapseTime += Time.deltaTime;
                yield return null;
            }
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