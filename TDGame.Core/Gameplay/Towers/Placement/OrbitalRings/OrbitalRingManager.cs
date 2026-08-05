using System;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;


public class OrbitalRingManager : SingletonComponent<OrbitalRingManager>
{
    private const float BaseOrbitalRingRadius = 1.5f;
    private readonly List<OrbitalRing> _orbitalRings = [];
    public int OrbitalRingCount => _orbitalRings.Count;

    public OrbitalRing CreateOrbitalRing()
    {
        var radius = BaseOrbitalRingRadius * (OrbitalRingCount + 1);
        
        var pos =
            SpaceTowersManager.Instance.PlanetEntity.Transform.WorldPosition2D +
            new Vector2(0, -radius);

        var ringEntity = Entity.Create("orbital ring", createAt: pos.ToVector3());
        
        var ring = ringEntity.AttachComponent<OrbitalRing>();
        ringEntity.AttachComponent<OrbitalRingDrawer>();
        
        
        ring.Radius = radius;

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
    ///     Gets the nearest ring to the point, returns null if none is near
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public OrbitalRing GetNearbyOrbitalRingFromPoint(Vector2 point)
    {
        foreach (var ring in _orbitalRings)
            if (ring.IsNearby(point))
                return ring;

        return null;
    }

    public OrbitalRing GetNearbyOrbitalRingFromMouse()
    {
        var mousePos = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
        return GetNearbyOrbitalRingFromPoint(mousePos);
    }

    public OrbitalRing GetFirstRing()
    {
        return _orbitalRings[0];
    }

    public IReadOnlyList<OrbitalRing> GetAllRings()
    {
        return _orbitalRings;
    }
}