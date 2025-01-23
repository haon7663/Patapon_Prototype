using UnityEngine;

namespace Actor.Unit.Component
{
    public class Movement : MonoBehaviour
    {
        private Unit _unit;
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float moveSpeed;

        public void Init(Unit unit)
        {
            _unit = unit;
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void SetVelocity(Vector2 dir)
        {
            Debug.Log($"Set Velocity to {dir}");
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y) + dir * moveSpeed;
        }

        public void StopImmediately()
        {
            Debug.Log("Stop immediately");
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
