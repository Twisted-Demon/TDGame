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

        var ent = CreateEntity(
            "entity",
            createAt: new Vector3(640f, 360f, 0f)
        );
        
        MainCamera.PixelsPerUnit = 1;
        MainCamera.SetTargetVerticalResolution(720);
        MainCamera.ForcePosition(new Vector3(640f , 360f, 0f));

        var rect = ent.AttachComponent<RectDrawer>();
        rect.Width = 128;
        rect.Height = 128;
        rect.Color = Color.Magenta;
    }
}