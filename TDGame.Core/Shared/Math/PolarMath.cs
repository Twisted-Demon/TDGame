using Dreambit;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public static class PolarMath
{
    public static Vector2 ToCartesian(float radius, float angleRadians)
    {
        return new Vector2(
            Mathf.Cos(angleRadians) * radius,
            Mathf.Sin(angleRadians) * radius);
    }

    public static Vector2 ToWorldPosition(Vector2 center, float radius, float angleRadians)
    {
        return center + ToCartesian(radius, angleRadians);
    }
}