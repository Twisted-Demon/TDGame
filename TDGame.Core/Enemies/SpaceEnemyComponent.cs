using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework.Input;
using TDGame.Core.Managers;

namespace TDGame.Core;

public abstract class SpaceEnemyComponent : Component
{
    public new TDGameScene Scene => (TDGameScene)base.Scene;
    public Entity Planet { get; set; }
    public float BaseVelocity { get; set; }

    public override void OnCreated()
    {
        Planet = SpaceDefenseManager.Instance.PlanetEntity;

        if (Planet is null)
            throw new ArgumentException(nameof(Planet));
    }

}