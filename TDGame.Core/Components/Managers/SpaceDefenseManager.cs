using System.Numerics;
using Dreambit;
using Dreambit.ECS;

namespace TDGame.Core.Managers;

[Require(typeof(ParticleTest))]
public class SpaceDefenseManager : SingletonComponent<SpaceDefenseManager>
{
    public Entity PlanetEntity { get; set; }

    public override void OnCreated()
    {
        CreatePlanet();
    }


    public void CreatePlanet()
    {
        var planetBp = Resources.LoadAsset<EntityBlueprint>("blueprints/terran_planet_bp");
        
        PlanetEntity = Scene.CreateEntity(planetBp);
        Scene.MainCamera.ForcePosition(PlanetEntity.Transform.Position);
        Logger.Info($"Created planet {PlanetEntity.Name} at {PlanetEntity.Transform.WorldPosition}");
    }
}