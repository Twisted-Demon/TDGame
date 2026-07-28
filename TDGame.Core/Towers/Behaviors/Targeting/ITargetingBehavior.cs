namespace TDGame.Core;

public interface ITargetingBehavior
{
    SpaceEnemyComponent SelectTarget(SpaceDefenseComponent self, SpaceEnemyComponent currentTarget = null);
}