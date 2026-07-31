using System.Globalization;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class TDGameScene : Scene<TDGameScene>
{
    public Entity PlanetEntity { get; private set; }
    
    protected override void OnInitialize()
    {
        Window.SetAllowUserResizing(true);
        
        SetUpCameraAndLighting();
        SetUpManagers();
    }
    
    private void SetUpCameraAndLighting()
    {
        AmbientLight.Intensity = 1.0f;
        AmbientLight.Color = Color.White;
        BackgroundColor = new Color(0, 4, 16);
        
        MainCamera.SetTargetVerticalResolution(864);
        MainCamera.PixelsPerUnit = 96;

        Window.WindowResized += (sender, args) =>
        {
            //MainCamera.SetTargetVerticalResolution(args.Height);
        };
    }

    private void SetUpManagers()
    {
        var enemyManager = CreateEntity("enemy_manager");
        enemyManager.AttachComponent<EnemyManager>();
        
        var spaceDefenseManager = CreateEntity("space_defense_manager");
        spaceDefenseManager.AttachComponent<SpaceDefenseManager>();
        
        var playerManager = CreateEntity("player_manager");
        playerManager.AttachComponent<PlayerManager>();
    }

    protected override void OnUpdate()
    {
        if(Input.IsKeyPressed(Keys.F7))
            DebugMode = !DebugMode;

        if (Input.GetScrollDelta() > 0)
            MainCamera.Zoom += 0.05f;
        if (Input.GetScrollDelta() < 0)
        {
            MainCamera.Zoom -= 0.05f;
            
        }

        if (Input.IsKeyPressed(Keys.Space))
        {
            Scene.SetNextScene<TDGameScene>();
        }
        
        MainCamera.Zoom = Mathf.Clamp(MainCamera.Zoom, 0.001f, 2.0f);
    }
}