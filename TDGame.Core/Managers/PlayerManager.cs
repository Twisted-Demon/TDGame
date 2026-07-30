using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core.Managers;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    public Entity PlacementIndicator;

    public EntityBlueprint SelectedDefenseBp;
    public string PlacementAnimPath;

    public override void OnCreated()
    {
        PlacementIndicator = Scene.CreateEntity("placement_indicator");
        var placementAnim = PlacementIndicator.AttachComponent<SpriteAnimator>();

        PlacementAnimPath = "animations/railgun/still_anim";
        placementAnim.AnimationPath = PlacementAnimPath;
        PlacementIndicator.GetComponent<SpriteDrawer>().WithOpacity(0.5f);

        SelectedDefenseBp = Resources.LoadAsset<EntityBlueprint>("blueprints/missile_launcher_bp");
    }

    public override void OnUpdate()
    {
        UpdatePlacementIndicator();
        
        if (Input.IsMousePressed(MouseButton.Left))
        {
            var spawnPosition = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
            var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;

            float distanceFromPlanet = 1.5f;

            var directionToSpawn = Vector2.Normalize(spawnPosition - planetPosition);
            
            var position = planetPosition + directionToSpawn * distanceFromPlanet;
            
            Entity.Create(SelectedDefenseBp, createAt: position.ToVector3());
            
            Logger.Info(position.ToString());
        }

        if (Input.IsKeyPressed(Keys.D1))
        {
            SelectedDefenseBp = Resources.LoadAsset<EntityBlueprint>("blueprints/missile_launcher_bp");
            PlacementAnimPath = "animations/missile_launcher/still_anim";

            PlacementIndicator
                .GetComponent<SpriteAnimator>().AnimationPath = PlacementAnimPath;
        }
        
        if (Input.IsKeyPressed(Keys.D2))
        {
            SelectedDefenseBp = Resources.LoadAsset<EntityBlueprint>("blueprints/railgun_bp");
            PlacementAnimPath = "animations/railgun/still_anim";
            
            PlacementIndicator
                .GetComponent<SpriteAnimator>().AnimationPath = PlacementAnimPath;
        }
    }

    private void UpdatePlacementIndicator()
    {
        var mousePos = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
        var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;

        float distanceFromPlanet = 1.5f;
        var directionToMouse = Vector2.Normalize(mousePos - planetPosition);
        
        var finalPos = planetPosition + directionToMouse * distanceFromPlanet;

        PlacementIndicator.Transform.Position2D = finalPos;
    }
    
}

public enum DefenseSatelliteType
{
    Railgun,
    MissileLauncher
}