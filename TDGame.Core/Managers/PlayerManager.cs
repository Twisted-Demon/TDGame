using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    public SpriteDrawer PlacementIndicator;
    public SpaceTowerDefinition SelectedTowerDefinition;

    public override void OnCreated()
    {
        PlacementIndicator = Entity.CreateChildOf(Entity, "placement_indicator")
            .AttachComponent<SpriteDrawer>().WithOpacity(0.5f);

        SelectedTowerDefinition 
            = SpaceDefenseManager.Instance.GetSpaceTowerDefinition("railgun");;
    }

    public override void OnUpdate()
    {
        UpdatePlacementIndicator();
        
        if (Input.LeftPressed())
        {
            var spawnPosition = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
            var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;

            float distanceFromPlanet = 1.5f;

            var directionToSpawn = Vector2.Normalize(spawnPosition - planetPosition);
            
            var position = planetPosition + directionToSpawn * distanceFromPlanet;

            if (!Component.IsNull(SpaceDefenseManager.Instance))
                SpaceDefenseManager.Instance.SpawnTower(SelectedTowerDefinition.Id, position.ToVector3());
            
            Logger.Info(position.ToString());
        }

        if (Input.IsKeyPressed(Keys.D1))
        {
            SelectedTowerDefinition 
                = SpaceDefenseManager.Instance.GetSpaceTowerDefinition("railgun");
        }
        
        if (Input.IsKeyPressed(Keys.D2))
        {
            SelectedTowerDefinition 
                = SpaceDefenseManager.Instance.GetSpaceTowerDefinition("missile_launcher");
        }
    }

    private void UpdatePlacementIndicator()
    {
        PlacementIndicator.Sprite = SelectedTowerDefinition.PlacementSprite;
        
        var mousePos = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
        var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;

        float distanceFromPlanet = 1.5f;
        var directionToMouse = Vector2.Normalize(mousePos - planetPosition);
        
        var finalPos = planetPosition + directionToMouse * distanceFromPlanet;

        PlacementIndicator.Transform.Position2D = finalPos;
    }
    
}