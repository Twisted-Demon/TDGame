using System.Collections.Generic;
using Dreambit.ECS;

namespace TDGame.Core;

public class OrbitalRing : Component
{
    public List<SpaceDefenseComponent> Defenses { get; set; } = [];

    public void RegisterDefense(SpaceDefenseComponent spaceDefense)
    {
        Defenses.Add(spaceDefense);
    }

    public void DeregisterDefense(SpaceDefenseComponent spaceDefense)
    {
        Defenses.Remove(spaceDefense);
    }
}