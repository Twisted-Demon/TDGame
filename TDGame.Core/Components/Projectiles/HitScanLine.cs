using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Projectiles;

public class HitScanLine : DrawableComponent<HitScanLine>
{
    public override RectangleF Bounds => Scene.MainCamera.BoundsF;
    
    public Vector3 Start { get; private set; }
    public Vector3 End { get; private set; }
    public Color Color { get; private set; }
    public float LifeTime { get; private set; }

    private float _lifeTimeTick;

    public static HitScanLine Create(Vector3 start, Vector3 end, Color color, float lifeTime)
    {
        var projectileObj = Entity.Create("hit_scan_line");
        var hitScanLine = projectileObj.AttachComponent<HitScanLine>();

        hitScanLine.Start = start;
        hitScanLine.End = end;
        hitScanLine.Color = color;
        hitScanLine.LifeTime = lifeTime;

        hitScanLine._lifeTimeTick = lifeTime;
        
        return hitScanLine;
    }
    
    public override void OnUpdate()
    {
        _lifeTimeTick -= Time.DeltaTime;
        var alpha = (_lifeTimeTick / LifeTime) * 255f;
        Color = new Color(Color.R, Color.G, Color.B, alpha);

        if (_lifeTimeTick < 0)
            Entity.Destroy(Entity);
    }

    public override void OnDraw()
    {
        Dreambit.Core.SpriteBatch.DrawLine(Start, End, Color, 2f * Scene.MainCamera.WorldUnitsPerTexturePixel);
    }
}