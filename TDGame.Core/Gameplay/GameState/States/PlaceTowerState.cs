using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class PlaceTowerState : State
{
    private Entity _placementIndicator;
    private SpriteDrawer _placementDrawer;
    protected new GameStateBlackboard Blackboard => (GameStateBlackboard)Fsm.Blackboard;

    public override void OnInitialize()
    {
        _placementIndicator = Entity.Create("placement-indicator");
        _placementDrawer = _placementIndicator.AttachComponent<SpriteDrawer>();
        
        _placementIndicator.Enabled = false;
        _placementDrawer.Enabled = false;
    }

    public override void OnEnter()
    {
        _placementIndicator.Enabled = true;
        _placementDrawer.Enabled = true;
        
        var towerDefinition = Blackboard.PlacementTowerDefinition;
        _placementDrawer.Sprite = towerDefinition.PlacementSprite;
        
        var mousePos = Scene.Instance.MainCamera.ScreenToWorld(Input.GetMousePosition());
        var ring = OrbitalRingManager.Instance.GetFirstRing();
        
        _placementIndicator.Transform.Position2D = PositionOnRing(ring, mousePos);
    }

    public override void OnExecute()
    {
        UpdatePlacementIndicator();
    }

    public override void OnEnd()
    {
        _placementIndicator.Enabled = false;
        _placementDrawer.Enabled = false;
    }

    private void UpdatePlacementIndicator()
    {
        //get our mouse position and see which rings are nearby
        var mousePos = Scene.Instance.MainCamera.ScreenToWorld(Input.GetMousePosition());
        var ring = OrbitalRingManager.Instance.GetNearbyOrbitalRingAtPoint(mousePos);

        if (ring is null) return; // return if none are nearby
        
        _placementIndicator.Transform.Position2D = PositionOnRing(ring, mousePos);
    }

    private Vector2 PositionOnRing(OrbitalRing ring, Vector2 mousePos)
    {
        var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosition2D;
        var radius = ring.Radius;
        var angle = Mathf.AngleBetweenVectors(planetPosition, mousePos);
        
        var finalPos = PolarMath.ToWorldPosition(planetPosition, radius, angle);
        
        return finalPos;
    }
}