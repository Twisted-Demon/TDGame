using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class SpaceDiverComponent : SpaceEnemyComponent
{
    private OrbitalDescentPath _path;

    public override void OnCreated()
    {
        base.OnCreated();

        var planetCenter = Planet.Transform.WorldPosition2D;
        var spawnPosition = Transform.WorldPosition2D;

        const float impactRadius = 0.5f;

        const int orbitDirection = 1;

        const float turns = 1.15f;

        _path = new OrbitalDescentPath(
            planetCenter,
            spawnPosition,
            impactRadius,
            orbitDirection,
            turns);
    }

    public override void OnUpdate()
    {
        SeekToPlanet();
    }

    private void SeekToPlanet()
    {
        _path.Update(1.65f);
        
        Transform.Position2D = _path.Position;
        Transform.Rotation2D = +_path.Forward.Angle();

        if (_path.IsComplete)
            EnemyManager.Instance.DestroyEnemy(this);
    }
}