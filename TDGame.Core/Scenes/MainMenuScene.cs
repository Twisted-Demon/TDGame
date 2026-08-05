using Dreambit;
using Dreambit.ECS;
using Dreambit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Core;

public class MainMenuScene : Scene
{
    protected override void OnInitialize()
    {
        InitializeSettings();
        CreateMainMenu();
    }

    private void InitializeSettings()
    {
        Window.SetAllowUserResizing(true);
        AmbientLight.Intensity = 1.0f;
        AmbientLight.Color = Color.White;

        RenderingOptions.UISamplerState = SamplerState.PointClamp;
        RenderingOptions.SamplerState = SamplerState.PointClamp;
    }

    private void CreateMainMenu()
    {
        var menuFrame = Entity.Create("main menu ui")
            .AttachComponent<UiFrame>()
            .WithLayout("UI/main-menu-ui.xml");

        var host = menuFrame.Layout;

        host.GetRequired<UiButton>("play-button").Clicked +=
            button => SetNextScene<TDGameScene>();

        host.GetRequired<UiButton>("exit-button").Clicked +=
            button => Dreambit.Core.Instance.Exit();
    }
}