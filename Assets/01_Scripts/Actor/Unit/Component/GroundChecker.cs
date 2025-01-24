using System;
using UniRx.Triggers;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Vector2 boxSize;

        [SerializeField] private LayerMask layerMask;

        public bool onGround;

        private Action _onGroundChecked;
        public event Action OnGroundChecked
        {
            add
            {
                _onGroundChecked -= value;
                _onGroundChecked += value;
            }
            remove => _onGroundChecked -= value;
        }

        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }
        
        private void FixedUpdate()
        {
            if (!onGround && CheckGround())
                _onGroundChecked?.Invoke();
            
            onGround = CheckGround();
        }

        private bool CheckGround()
        {
            return Physics2D.OverlapBox(transform.position + offset, boxSize, 0, layerMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + offset, boxSize);
        }
    }
}
