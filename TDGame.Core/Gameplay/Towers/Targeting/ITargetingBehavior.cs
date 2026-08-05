using System.Collections.Generic;
using Dreambit.ECS;

namespace TDGame.Core;

public interface ITargetingBehavior
{
    SpaceEnemyComponent SelectTarget(Transform self, float range, IReadOnlyList<string> tags);
}