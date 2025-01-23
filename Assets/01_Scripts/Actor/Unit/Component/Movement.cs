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
            if (dir != Vector2.zero)
                transform.rotation = Quaternion.Euler(0, dir.x > 0 ? 0 : 180, 0);
        }

        public void StopImmediately(bool ignoreYAxis = false)
        {
            Debug.Log("Stop immediately");
            _rigidbody.linearVelocity = ignoreYAxis ? new Vector2(0, _rigidbody.linearVelocity.y) : Vector2.zero;
        }
    }
}
