using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Core;

public class TDGameScene : Scene<TDGameScene>
{
    public Entity PlanetEntity { get; private set; }
    
    protected override void OnInitialize()
    {
        SetUpCameraAndLighting();
        SetUpPlanet();
        SetUpBackground();
    }
    
    private void SetUpCameraAndLighting()
    {
        AmbientLight.Intensity = 1.0f;
        AmbientLight.Color = Color.White;
        
        MainCamera.SetTargetVerticalResolution(720);
        MainCamera.PixelsPerUnit = 64;
    }

    private void SetUpPlanet()
    {
        var terranPlanetBp = Resources.LoadAsset<EntityBlueprint>("blueprints/planets/terran_planet");
        
        PlanetEntity = CreateEntity(terranPlanetBp, createAt: new Vector3(20.0f * 0.5f, 11.25f * 0.5f, 0.0f));

        MainCamera.ForcePosition(PlanetEntity.Transform.WorldPosition);
    }

    private void SetUpBackground()
    {
        var background = CreateEntity("background");
        var drawer = background.AttachComponent<SpriteDrawer>().WithPivot(PivotType.TopLeft);

        drawer.Sprite = Sprite.Create("Textures/backgrounds/default_background");
        drawer.DrawLayer = -900;
        drawer.Sprite.PixelsPerUnit = 64;
    }

    protected override void OnUpdate()
    {
        if (Input.IsMousePressed(MouseButton.Right))
        {
            var diverBp = Resources.LoadAsset<EntityBlueprint>("blueprints/enemies/space_diver");

            var spawnPosition = MainCamera.ScreenToWorld(Input.GetMousePosition());
            CreateEntity(diverBp, createAt: spawnPosition.ToVector3());

            Logger.Info(spawnPosition.ToString());
        }
    }
}