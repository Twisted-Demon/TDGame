using System;
using Dreambit;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public sealed class SpawnSector
{
    public SpawnSector(
        float minimumRadius,
        float maximumRadius,
        float minimumAngleDegrees,
        float maximumAngleDegrees)
    {
        if(minimumAngleDegrees < 0f)
            throw new ArgumentOutOfRangeException(nameof(minimumAngleDegrees));
        
        if(maximumAngleDegrees < minimumAngleDegrees)
            throw new ArgumentOutOfRangeException(nameof(maximumAngleDegrees));
        
        MinimumRadius = minimumRadius;
        MaximumRadius = maximumRadius;
        MinimumAngleDegrees = minimumAngleDegrees;
        MaximumAngleDegrees = maximumAngleDegrees;
    }
    
    public float MinimumRadius { get; }

    public float MaximumRadius { get; }

    public float MinimumAngleDegrees { get; }

    public float MaximumAngleDegrees { get; }

    public Vector2 GeneratePosition(Vector2 center)
    {
        var radius = Random.Shared.NextFloat(
            MinimumRadius,
            MaximumRadius);

        var angleDegrees = Random.Shared.NextFloat(
            MinimumAngleDegrees,
            MaximumAngleDegrees);
        
        var angleRadians = Mathf.Radians(angleDegrees);
        
        return center.ToPolar(radius, angleRadians);
    }
}
