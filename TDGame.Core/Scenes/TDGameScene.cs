using System.Globalization;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        CreateEntity("game audio manager")
            .AttachComponent<GameAudioManager>();

        CreateEntity("enemy manager")
            .AttachComponent<EnemyManager>();
        
        CreateEntity("space defense manager")
            .AttachComponent<SpaceDefenseManager>();

        var ring = CreateEntity("orbital ring manager")
            .AttachComponent<OrbitalRingManager>();

        ring.CreateOrbitalRing();
        ring.CreateOrbitalRing();
        
        CreateEntity("player manager")
            .AttachComponent<PlayerManager>();
        
        CreateEntity("wave director")
            .AttachComponent<WaveDirectorComponent>();
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

        if (Input.IsKeyHeld(Keys.T))
        {
            Time.TimeScale = 2.5f;
        }
        else
        {
            Time.TimeScale = 1f;
        }

        if (Input.IsKeyPressed(Keys.Space))
        {
            Scene.SetNextScene<TDGameScene>();
        }
        
        MainCamera.Zoom = Mathf.Clamp(MainCamera.Zoom, 0.001f, 2.0f);
    }
}