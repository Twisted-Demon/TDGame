using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Core;

public class GameScene : Scene<GameScene>
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
        
        MainCamera.PixelsPerUnit = 1;
        MainCamera.SetTargetVerticalResolution(720);
        MainCamera.ForcePosition(new Vector3(640f , 360f, 0f));
    }

    private void SetUpPlanet()
    {
        var ent = CreateEntity(
            "planet",
            createAt: new Vector3(640f, 360f, 0f)
        );
        
        var rect = ent.AttachComponent<SpriteDrawer>();
        var anim = ent.AttachComponent<SpriteAnimator>();
        var ring = ent.AttachComponent<WorldRingDrawer>();
        ring.Radius = 64f;
        
        anim.SetAnimation("Animations/terran");
        anim.Play();

        var diverBp = Resources.LoadAsset<EntityBlueprint>("blueprints/space_diver");

        CreateEntity(diverBp, createAt: new Vector3(520, 360, 0f));
    }

    private void SetUpBackground()
    {
        var background = CreateEntity("background");
        var drawer = background.AttachComponent<SpriteDrawer>().WithPivot(PivotType.TopLeft);

        drawer.Sprite = Sprite.Create("Textures/backgrounds/default_background");
        drawer.DrawLayer = -900;
    }
    
    
}