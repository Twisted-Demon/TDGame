using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class TargetNearest : ITargetingBehavior
{
    public SpaceEnemyComponent SelectTarget(Transform self, float range, IReadOnlyList<string> tags)
    {
        var rangeSquared = range * range;

        SpaceEnemyComponent nearest = null;
        var nearestDistanceSquared = float.MaxValue;

        if (!PhysicsSystem.Instance.CircleCastByTag(
                self.WorldPosition2D,
                range,
                out var inRange,
                tags)) return null;

        foreach (var collider in inRange.Collisions)
        {
            var enemy = collider.Entity.GetComponent<SpaceEnemyComponent>();

            if (enemy is null || Entity.IsDestroyed(enemy.Entity)) continue;

            var defensePosition = self.WorldPosition;
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