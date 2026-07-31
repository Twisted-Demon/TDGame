using System;
using System.Linq;
using Dreambit;
using Dreambit.ECS;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class HomingProjectile : Projectile
{
    public Entity Target { get; set; }

    public float TurnSpeed { get; set; } = 120f;

    public ITargetingBehavior RetargetBehavior { get; set; } = new TargetNearest();

    protected override void Seek()
    {
        if (!Entity.IsDestroyed(Target))
        {
            var turnSpeedRad = Mathf.Radians(TurnSpeed);
            
            var directionToTarget =
                Target.Transform.WorldPosition2D - Transform.WorldPosition2D;

            if (directionToTarget.LengthSquared() <= Mathf.Epsilon)
                return;
            
            //rotate by turn speed per second
            Transform.RotateTowards2D(directionToTarget, turnSpeedRad * Time.DeltaTime);
        }
        
        Transform.MoveForward2D(InitialVelocity *  Time.DeltaTime);

        if (PhysicsSystem.Instance.PointCastByTag(Transform.WorldPosition2D, out var collisionResult, ["enemy"]))
        {
            if (collisionResult.Count >= 0)
            {
                var enemyCollider = collisionResult.Collisions[0];
                var enemy = enemyCollider.Entity.GetComponent<SpaceEnemyComponent>();

                if (enemy is not null)
                {
                    EnemyManager.Instance.DestroyEnemy(enemy);
                    Entity.Destroy(Entity);
                }
            }
        }
    }
}