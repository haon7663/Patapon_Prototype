using UnityEngine;

namespace Actor.Unit.Component
{
    public class EnemyAgent : Agent
    {
        private void LateUpdate()
        {
            var target = SearchTarget();
            if (target)
            {
                switch (AutoAttack.InRange)
                {
                    case true when AutoAttack.attackCooldown <= 0:
                        onAttack?.Invoke();
                        break;
                    case true:
                        onMovement?.Invoke(Vector2.zero);
                        break;
                    default:
                        dir = target.position.x - transform.position.x > 0 ? Vector2.right : Vector2.left;
                        onMovement?.Invoke(dir);
                        break;
                }
            }
            else
            {
                dir = Vector2.left;
                onMovement?.Invoke(dir);
            }
        }
    }
}