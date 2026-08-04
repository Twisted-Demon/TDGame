using System;
using System.Collections.Generic;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[Require(typeof(OrbitalRingDrawer))]
public class OrbitalRingManager : SingletonComponent<OrbitalRingManager>
{
    private readonly List<OrbitalRing> _orbitalRings = [];
    public int OrbitalRingCount => _orbitalRings.Count;

    private const float BaseOrbitalRingRadius = 1.5f;

    public OrbitalRing CreateOrbitalRing()
    {
        var ring = new OrbitalRing
        {
            Radius = BaseOrbitalRingRadius * (OrbitalRingCount + 1)
        };
        
        _orbitalRings.Add(ring);

        return ring;
    }

    public OrbitalRing GetOrbitalRingAtIndex(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, OrbitalRingCount);

        return _orbitalRings[index];
    }

    /// <summary>
    /// Gets the nearest ring to the point, returns null if none is near
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public OrbitalRing GetNearbyOrbitalRingAtPoint(Vector2 point)
    {
        foreach (var ring in _orbitalRings)
        {
            if (ring.IsNearby(point))
                return ring;
        }
        
        return null;
    }
    
    public OrbitalRing GetFirstRing() => _orbitalRings[0];

    public IReadOnlyList<OrbitalRing> GetAllRings() => _orbitalRings;
}
