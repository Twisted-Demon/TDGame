using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class TargetNearest : ITargetingBehavior
{
    public SpaceEnemyComponent SelectTarget(SpaceDefenseComponent self)
    {
        var range = self.Range;
        var rangeSquared = range * range;

        SpaceEnemyComponent nearest = null;
        var nearestDistanceSquared = float.MaxValue;

        if (!PhysicsSystem.Instance.CircleCastByTag(
                self.Transform.WorldPosition2D,
                range,
                out var inRange,
                ["enemy"])) return null;

        foreach (var collider in inRange.Collisions)
        {
            var enemy = collider.Entity.GetComponent<SpaceEnemyComponent>();

            if (enemy is null || Entity.IsDestroyed(enemy.Entity)) return null;
            
            var defensePosition = self.Transform.WorldPosition;
            var enemyPosition = enemy.Transform.WorldPosition;

            var distanceSquared = Vector3.DistanceSquared(defensePosition, enemyPosition);

            if (distanceSquared > rangeSquared)
                continue;

            if (distanceSquared < nearestDistanceSquared)
            {
                nearest = enemy;
                nearestDistanceSquared = distanceSquared;
            }
        }
        
        return nearest;
    }
}