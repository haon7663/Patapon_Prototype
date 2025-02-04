using System;
using Actor.Unit.Enums;
using UnityEngine;

namespace Actor.Unit.Component
{
    public class AllianceAgent : Agent
    {
        private void LateUpdate()
        {
            var target = SearchTarget();
            if (target)
            {
                if (AutoAttack.InRange && AutoAttack.attackCooldown <= 0)
                {
                    onAttack?.Invoke();
                }
                else
                {
                    if (AutoAttack.InNearRange)
                    {
                        dir = target.position.x > transform.position.x ? Vector2.right : Vector2.left;
                        onMovement?.Invoke(dir);
                        
                        if (AutoAttack.InRange)
                            onMovement?.Invoke(Vector2.zero);
                    }
                    else
                    {
                        var boundaryX = AllianceActing.GetBoundaryPositionX(Unit);
                        if (Mathf.Abs(boundaryX - transform.position.x) < 0.1f)
                        {
                            onMovement?.Invoke(Vector2.zero);
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            dir = boundaryX > transform.position.x ? Vector2.right : Vector2.left;
                            onMovement?.Invoke(dir);
                        }
                    }
                }
            }
            else
            {
                var boundaryX = AllianceActing.GetBoundaryPositionX(Unit);
                if (Mathf.Abs(boundaryX - transform.position.x) < 0.1f)
                {
                    onMovement?.Invoke(Vector2.zero);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    dir = boundaryX > transform.position.x ? Vector2.right : Vector2.left;
                    onMovement?.Invoke(dir);
                }
            }
        }
    }
}