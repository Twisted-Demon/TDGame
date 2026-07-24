using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Core.Scenes;

public class GameScene : Scene<GameScene>
{
    protected override void OnInitialize()
    {
        Window.SetSize(1280, 720);
        
        RenderingOptions.SamplerState = SamplerState.PointClamp;
        
        AmbientLight.Intensity = 1.0f;
        AmbientLight.Color = Color.White;

        var ent = CreateEntity("entity");

        var sprite = ent.AttachComponent<SpriteDrawer>();
        
        MainCamera.PixelsPerUnit = 1;
        MainCamera.SetTargetVerticalResolution(720);
        MainCamera.ForcePosition(new Vector3(640f , 360f, 0f));

        sprite.Sprite = Sprite.Create("Textures/grid");
    }
}