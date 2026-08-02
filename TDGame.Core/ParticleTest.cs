using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[Require(typeof(ParticleSystemDrawer))]
public class ParticleTest : Component<ParticleTest>
{
    [FromRequired]
    public ParticleSystemDrawer PSystem { get; set; }

    private ParticleFxConfig _config;

    public override void OnCreated()
    {
        PSystem.TexturePath = "shared/particles/white-pixel.texture";

        _config = new ParticleFxConfig
        {
            // Emit continuously; keep it modest so it looks natural
            EmissionMode = EmissionMode.Burst,
            EmissionRate = 2500,

            // Spawn from a small area a bit above the flame center
            SpawnType = ParticleSpawnType.Point,

            // Lifetimes: short puffs mixed with longer wisps
            LifeTime = new RangeF(10f, 15f),

            // Initial motion: mostly upward, with a bit of random speed
            StartSpeed = new RangeF(0f, 0f),

            // Sizes: start small, grow via SizeOverLife
            StartSize = new Range2(new(1f, 1f), new(1f, 1f)),

            // Randomized orientation and a little spin
            StartRotationDeg = new RangeF(0f, 360f),
            StartSpin = new RangeF(-0.6f, 0.6f),

            // Soft gray-brown—adjust in your texture or here if you want warmer smoke
            StartColor = new Color(110, 105, 100, 180),

            // Spawn jitter: spread around the ember bed a tiny bit
            PositionJitter = new Range2(new(-16f, -20f), new(16f, 20f)),

            // Velocity jitter: gentle sideways curls
            VelocityJitter = new Range2(new(-0.5f, -0.5f), new(0.5f, 0.5f)),

            // “Buoyancy”: negative Y gravity for upward lift (MonoGame Y+ is down)
            Gravity = Vector2.Zero,

            // Light damping to slow over time without killing motion instantly
            LinearDamping = 1.2f,

            // Curves (t = 0..1 of particle life)
            // Alpha: quick fade-in, hold, then fade out
            AlphaOverLife = new Curve1D(
                new Curve1D.Key(0.00f, 0.00f),
                new Curve1D.Key(0.10f, 0.90f),
                new Curve1D.Key(0.70f, 0.90f),
                new Curve1D.Key(1.00f, 0.00f)
            ),

            // Size: expand as it rises, then stabilize a bit near the end
            SizeOverLife = new Curve1D(
                new Curve1D.Key(0.00f, 0.55f),
                new Curve1D.Key(0.15f, 0.90f),
                new Curve1D.Key(0.60f, 1.30f),
                new Curve1D.Key(1.00f, 1.60f)
            ),

            // Speed: strongest at birth, then slows as the puff diffuses
            SpeedOverLife = new Curve1D(
                new Curve1D.Key(0.00f, 1.00f),
                new Curve1D.Key(0.40f, 0.65f),
                new Curve1D.Key(1.00f, 0.35f)
            ),

            // Spin: slight decay to avoid aggressive twirling
            SpinOverLife = new Curve1D(
                new Curve1D.Key(0.00f, 1.00f),
                new Curve1D.Key(1.00f, 0.40f)
            ),
            Bursts = new List<Burst>
            {
                new Burst
                {
                    Time = 5f,
                    Count = 250,
                    Cycles = 10000,
                    Interval = 0.0f
                },
            }
        };
        
        PSystem.Simulation.SetParticleFxConfig(_config);
        PSystem.Simulation.Emit();
    }
}
