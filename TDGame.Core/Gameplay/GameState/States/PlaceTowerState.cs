using Dreambit;
using Dreambit.ECS;
using Dreambit.Events;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core;

//TODO: check if we are nearby
public class PlaceTowerState : State
{
    private SpriteDrawer _placementDrawer;
    private Entity _placementIndicator;
    private new GameStateBlackboard Blackboard => (GameStateBlackboard)Fsm.Blackboard;

    public override void OnInitialize()
    {
        RegisterTransitions();
        
        _placementIndicator = Entity.Create("placement-indicator");
        _placementDrawer = _placementIndicator.AttachComponent<SpriteDrawer>();
        _placementDrawer.WithOpacity(0.25f).WithPivot(PivotType.Center);

        _placementIndicator.Enabled = false;
        _placementDrawer.Enabled = false;
    }

    public override void OnEnter()
    {
        _placementIndicator.Enabled = true;
        _placementDrawer.Enabled = true;
        
        UpdatePlacementIndicatorSprite();
        var ring = OrbitalRingManager.Instance.GetFirstRing();

        _placementIndicator.Transform.Position2D = ring.MousePositionOnRing();
    }

    public override void OnExecute()
    {
        UpdatePlacementIndicatorSprite();
        UpdatePlacementIndicatorPosition();
    }

    public override void OnEnd()
    {
        _placementIndicator.Enabled = false;
        _placementDrawer.Enabled = false;
    }
    
    public override bool Reason()
    {
        if (Input.RightPressed() || Input.IsKeyPressed(Keys.Escape))
        {
            Fsm.SetNextState<BrowseInteractionState>();
            return false;
        }

        return true;
    }

    private void UpdatePlacementIndicatorSprite()
    {
        var towerDefinition = Blackboard.TowerDefinitionForPlacement.Value;
        _placementDrawer.Sprite = towerDefinition.PlacementSprite;
    }

    private void UpdatePlacementIndicatorPosition()
    {
        //get our mouse position and see which rings are nearby

        var ring = OrbitalRingManager.Instance.GetNearbyOrbitalRingFromMouse();

        if (ring is null) return; // return if none are nearby

        var positionOnRing = ring.MousePositionOnRing();

        _placementIndicator.Transform.WorldPosition2D = positionOnRing;

        if (Input.LeftPressed())
        {
            var towerDefinition = Blackboard.TowerDefinitionForPlacement.Value;

            var tower = SpaceTowersManager.Instance.SpawnTower(
                towerDefinition.Id,
                positionOnRing.ToVector3(),
                ring);

            Blackboard.TowerDefinitionForPlacement.Value = null;
            
            var towerPlacedEventArgs = new TowerPlacedEventArgs
            {
                Tower = tower
            };
            EventBus.Instance.Invoke(TowerPlacedEvent.Instance, towerPlacedEventArgs);
            Fsm.Trigger<TowerPlacedEvent>();
        }
    }

    private void RegisterTransitions()
    {
        Fsm.AddTransition<PlaceTowerState, BrowseInteractionState>(fsm =>
        {
            var eventOccurred = fsm.TryConsumeEvent<TowerPlacedEvent>();
            var isNull = Blackboard.TowerDefinitionForPlacement.Value == null;

            return eventOccurred && isNull;
        });
    }
}