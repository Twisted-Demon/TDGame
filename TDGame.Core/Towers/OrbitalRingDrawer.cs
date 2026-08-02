using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class OrbitalRingDrawer : DrawableComponent
{
    public override RectangleF Bounds { get; } = Scene.Instance.MainCamera.BoundsF;

    public override void OnCreated()
    {
        DrawLayer = -100;
    }

    public override void OnDraw()
    {
        var orbitalRings = OrbitalRingManager.Instance.GetAllRings();

        foreach (var ring in orbitalRings)
        {
            var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;
            
            Dreambit.Core.SpriteBatch.DrawHollowCircle(
                planetPosition,
                ring.Radius,
                Color.White * 0.35f,
                segments: 128,
                thickness: 1.25f * Scene.MainCamera.WorldUnitsPerTexturePixel 
                * (1 / Scene.MainCamera.Zoom)
                );
        }
    }
}