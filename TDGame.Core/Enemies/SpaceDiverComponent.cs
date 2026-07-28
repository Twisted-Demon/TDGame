using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Managers;

namespace TDGame.Core;

[Require(typeof(Mover), typeof(SpaceEnemyComponent))]
public class SpaceDiverComponent : Component
{
    [FromRequired]
    public Mover Mover { get; set; }
    
    [FromRequired]
    public SpaceEnemyComponent SpaceEnemyComponent { get; set; }
    
    public Entity Planet { get; set; }
    
    public new TDGameScene Scene { get; set; }

    public override void OnCreated()
    {
        Scene = Dreambit.Core.Instance.CurrentScene as TDGameScene;
        
        if(Scene == null)
            throw new ArgumentNullException(nameof(Scene));
        
        Planet = SpaceDefenseManager.Instance.PlanetEntity;
    }

    public override void OnUpdate()
    {
        SeekToPlanet();
    }

    private void SeekToPlanet()
    {
        var planetPos = Planet.Transform.WorldPosition;

        var dirToPlanet = (planetPos - Transform.WorldPosition);
        var dirToPlanetNormalized = Vector3.Normalize(dirToPlanet);

        Mover.Velocity = dirToPlanetNormalized * 1.25f;
        
        var angle = Mathf.AngleBetweenVectors(Transform.WorldPosToVec2, planetPos.ToVector2());

        Transform.Rotation.Z = angle;

        if (Vector3.Distance(planetPos, Transform.WorldPosition) <= 0.5f)
        {
            EnemyManager.Instance.DestroyEnemy(SpaceEnemyComponent);
        }
    }
}