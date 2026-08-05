using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;


public class OrbitalRingDrawer : DrawableComponent
{
    public override RectangleF Bounds { get; } = Scene.Instance.MainCamera.BoundsF;
    
    private OrbitalRing OrbitalRing { get; set; }

    public override void OnCreated()
    {
        DrawLayer = -100;
        
        OrbitalRing = Entity.GetComponent<OrbitalRing>();
    }

    protected override void OnDraw()
    {
        var planetPosition = SpaceTowersManager.Instance.PlanetEntity.Transform.WorldPosition2D;

        Dreambit.Core.SpriteBatch.DrawHollowCircle(
            planetPosition,
            OrbitalRing.Radius,
            Color.White * 0.35f,
            segments: 128,
            thickness: 1.25f * Scene.MainCamera.WorldUnitsPerTexturePixel
                             * (1 / Scene.MainCamera.Zoom)
        );
    }
}