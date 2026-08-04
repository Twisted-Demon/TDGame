using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class OrbitalRing
{
    private readonly HashSet<SpaceTowerComponent> _activeTowers = [];
    
    public bool HasTowers => _activeTowers.Count != 0;
    
    private Entity PlanetEntity { get; } = SpaceDefenseManager.Instance.PlanetEntity;

    public float Radius { get; set; } = 1.5f;

    public void RegisterTower(SpaceTowerComponent tower, float angleRadians)
    {
        if (_activeTowers.Contains(tower))
            return;
        
        var planetPosition 
            = PlanetEntity.Transform.WorldPosition2D;
        
        var orbitalPosition = PolarMath.ToWorldPosition(planetPosition, Radius, angleRadians);
        tower.Entity.Transform.WorldPosition2D = orbitalPosition;
        
        _activeTowers.Add(tower);
    }
    

    public void UnregisterTower(SpaceTowerComponent tower)
    {
        if (tower is null) return;
        
        _activeTowers.Remove(tower);
    }

    public bool IsNearby(Vector2 point, float tolerance = 0.25f)
    {
        var planetPosition = 
            PlanetEntity.Transform.WorldPosition2D;

        var distance = Vector2.Distance(planetPosition, point);
        var distanceToRadius = Mathf.Abs(Radius - distance);

        return distanceToRadius <= tolerance;
    }
}
