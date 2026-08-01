using Dreambit;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public static class Vector2Extensions
{
    public static Vector2 ToPolar(this Vector2 center, float radius, float angleRadians)
    {
        var x = Mathf.Cos(angleRadians) * radius;
        var y = Mathf.Sin(angleRadians) * radius;
        
        return center + new Vector2(x, y);
    }
}