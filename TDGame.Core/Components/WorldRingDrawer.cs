using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Core;

public class WorldRingDrawer : DrawableComponent
{
    public override Rectangle Bounds => GetBounds();
    
    private static readonly Vector2 PixelOrigin = new Vector2(0.5f, 0.5f);

    private Texture2D WhitePixel => SpriteBatchExtensions.PixelTexture;
    
    private EffectParameter _quadSizeWorldParameter;
    private EffectParameter _radiusWorldParameter;
    private EffectParameter _thicknessWorldParameter;
    private EffectParameter _ringColorParameter;
    private EffectParameter _softnessWorldParameter;
    private EffectParameter _opacityParameter;
    private EffectParameter _dashCountParameter;
    private EffectParameter _dashFillParameter;
    private EffectParameter _dashOffsetParameter;
    private EffectParameter _roundDashCaps;

    public float Radius { get; set; } = 64.0f;
    public float Thickness { get; set; } = 1.0f;
    public Color Color { get; set; } = Color.White;
    public float Opacity { get; set; } = 1.0f;
    public float Softness { get; set; } = 0f;
    public float DashCount { get; set; } = 0f;
    public float DashFill { get; set; } = 1f;
    public float DashOffsetRadians { get; set; } = 0f;

    public override void OnCreated()
    {
        Effect = Resources.LoadAsset<Effect>("Effects/WorldRing");
        
        _quadSizeWorldParameter =
            GetRequiredParameter("QuadSizeWorld");

        _radiusWorldParameter =
            GetRequiredParameter("RadiusWorld");

        _thicknessWorldParameter =
            GetRequiredParameter("ThicknessWorld");

        _ringColorParameter =
            GetRequiredParameter("RingColor");

        _softnessWorldParameter =
            GetRequiredParameter("SoftnessWorld");

        _opacityParameter =
            GetRequiredParameter("Opacity");

        _dashCountParameter =
            GetRequiredParameter("DashCount");

        _dashFillParameter =
            GetRequiredParameter("DashFill");

        _dashOffsetParameter =
            GetRequiredParameter("DashOffsetRadians");
        
        _roundDashCaps = GetRequiredParameter("RoundDashCaps");

        SpriteBatchExtensions.EnsurePixelTextureExists(Dreambit.Core.GraphicsDeviceManager.GraphicsDevice);
    }
    
    private EffectParameter GetRequiredParameter(string name)
    {
        return Effect.Parameters[name]
               ?? throw new InvalidOperationException(
                   $"WorldRing.fx does not contain parameter '{name}'.");
    }

    public override void OnDraw()
    {
        if (Radius < 0.0f)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Radius), "Radius must be greater than zero.");
        }

        if (Thickness <= 0.0f)
            return;

        Opacity = Mathf.Clamp(Opacity, 0f, 1.0f);
        Softness = Mathf.Max(Softness, 0.0f);
        DashCount = Mathf.Max(DashCount, 0.0f);
        DashFill = Mathf.Clamp(DashFill, 0.0f, 1.0f);

        float halfThickness = Thickness * 0.5f;

        float twoPixelsInWorldUnits = 2f / Scene.MainCamera.Scale;
        
        float padding = Mathf.Max(Softness, twoPixelsInWorldUnits);
        
        float halfQuadSizeWorld = Radius + halfThickness + padding;

        float quadSizeWorld = halfQuadSizeWorld * 2f;
        
        _quadSizeWorldParameter.SetValue(
            new Vector2(quadSizeWorld, quadSizeWorld));

        _radiusWorldParameter.SetValue(Radius);
        _thicknessWorldParameter.SetValue(Thickness);
        _ringColorParameter.SetValue(Color.ToVector4());
        _softnessWorldParameter.SetValue(Softness);
        _opacityParameter.SetValue(Opacity);
        _dashCountParameter.SetValue(DashCount);
        _dashFillParameter.SetValue(DashFill);
        _dashOffsetParameter.SetValue(DashOffsetRadians);
        _roundDashCaps.SetValue(1f);
        
        Dreambit.Core.SpriteBatch.Draw(
                texture: WhitePixel,
                position: Transform.WorldPosToVec2,
                sourceRectangle: null,
                color: Color,
                rotation: 0f,
                origin: PixelOrigin,
                scale: new Vector2(quadSizeWorld, quadSizeWorld),
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
    }

    private Rectangle GetBounds()
    {
        var pivotToUse = Transform.WorldPosToVec2;

        var pivotOffset = PivotHelper.GetRelativePivot(PivotType.Center);
        pivotToUse -= new Vector2(pivotOffset.X * Radius, pivotOffset.Y * Radius);
        
        var bounds = new Rectangle(
            (int)pivotToUse.X,
            (int)pivotToUse.Y,
            (int)Radius,
            (int)Radius);

        return bounds;
    }
}