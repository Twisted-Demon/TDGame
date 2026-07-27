using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class TargetNearest : ITargetingBehavior
{
    public SpaceEnemyComponent SelectTarget(SpaceDefenseComponent self, SpaceEnemyComponent currentTarget = null)
    {
        var range = self.Range;
        var rangeSquared = range * range;

        SpaceEnemyComponent nearest = null;
        var nearestDistanceSquared = float.MaxValue;

        foreach (var enemy in EnemyManager.Instance.ActiveEnemies)
        {
            if (Entity.IsDestroyed(enemy.Entity))
                continue;

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