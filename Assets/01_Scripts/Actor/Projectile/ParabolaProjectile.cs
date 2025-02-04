using System.Collections;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actor.Projectile
{
    public class ParabolaProjectile : Projectile
    {
        [SerializeField] [Range(0, 1f)] private float randomizeTargetAmount;
        [SerializeField] private float height;
        [SerializeField] private float duration;
        
        private float _damage;
        private Alliances _targetAlliance = Alliances.None;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        
        public override void Init(Transform owner, Transform target, float damage)
        {
            _startPosition = transform.position + Vector3.up * 0.25f;
            _endPosition = target.position + Vector3.up * 0.25f + Vector3.right * Random.Range(-randomizeTargetAmount, randomizeTargetAmount);;
            
            _damage = damage;
            _targetAlliance = owner.GetComponent<AllianceAgent>().targetAlliance;

            StartCoroutine(Parabola());
        }

        private IEnumerator Parabola()
        {
            float distance = _endPosition.x - _startPosition.x;
            
            float vX = distance / duration;
            float gravity = 8 * height / (duration * duration);
            float vY = gravity * duration / 2;
            
            float elapseTime = 0;

            while (elapseTime < duration)
            {
                var x = vX * elapseTime;
                var y = vY * elapseTime - 0.5f * gravity * elapseTime * elapseTime;

                var previousPosition = transform.position;
                var currentPosition = new Vector3(_startPosition.x + x, _startPosition.y + y, transform.position.z);
                
                var direction = currentPosition - previousPosition;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                transform.rotation = Quaternion.Euler(0, 0, angle);
                
                transform.position = new Vector3(_startPosition.x + x, _startPosition.y + y, transform.position.z);

                elapseTime += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
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