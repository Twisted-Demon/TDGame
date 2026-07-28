using System.Collections;
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

    private float _opacity = 1.0f;

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

    public override void OnAddedToEntity()
    {
        CoroutineService.StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (_opacity > 0)
        {
            _opacity -= Time.DeltaTime * 5.5f;

            yield return null;
        }
    }

    public override void OnUpdate()
    {
        if (_opacity <= 0)
            Entity.Destroy(Entity);
    }

    public override void OnDraw()
    {
        Dreambit.Core.SpriteBatch.DrawLine(Start, End, Color * _opacity, 1f * Scene.MainCamera.WorldUnitsPerTexturePixel);
    }
}