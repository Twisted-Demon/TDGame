using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[BlueprintType(nameof(SpaceDiverComponent))]
public class SpaceDiverComponent : SpaceEnemyComponent
{
    private OrbitalDescentPath _path;

    protected override void OnSpawnReady()
    {
        var planetCenter = Planet.Transform.WorldPosition2D;
        
        var spawnPosition = Transform.WorldPosition2D;

        const float impactRadius = 0.5f;
        const int orbitDirection = -1;
        const float turns = 0.5f;

        _path = new OrbitalDescentPath(
            planetCenter,
            spawnPosition,
            impactRadius,
            orbitDirection,
            turns);
    }
    
    public override void OnUpdate()
    {
        if (_path is null)
            return;
        
        SeekToPlanet();
    }

    private void SeekToPlanet()
    {
        _path.Update(MovementSpeed);
        
        Transform.Position2D = _path.Position;
        Transform.Rotation2D = +_path.Forward.Angle();

        if (_path.IsComplete)
            EnemyManager.Instance.DestroyEnemy(this);
    }
}
