using System;
using Dreambit;
using Dreambit.ECS;
using Dreambit.Events;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core;

public class RepositionTowerState : State
{
    private SpriteDrawer _movementDrawer;
    private Entity _movementIndicator;
    private SpaceTowerComponent _towerToMove;

    public override void OnInitialize()
    {
        _movementIndicator = Entity.Create("movement indicator");
        _movementDrawer = _movementIndicator.AttachComponent<SpriteDrawer>()
            .WithOpacity(0.25f).WithPivot(PivotType.Center);

        _movementIndicator.Enabled = false;
        _movementDrawer.Enabled = false;

    }

    public override void OnEnter()
    {
        _movementIndicator.Enabled = true;
        _movementDrawer.Enabled = true;

        _movementDrawer.Sprite = _towerToMove.Blackboard.TowerDefinition.PlacementSprite;
    }

    public override void OnExecute()
    {
        if (CheckForCancel())
        {
            Fsm.GoToDefault();
            return;
        }
        
        UpdateMovementIndicator();
    }

    private bool CheckForCancel()
    {
        return Input.RightPressed() || Input.IsKeyPressed(Keys.Escape);
    }

    private void UpdateMovementIndicator()
    {
        var tower = 
            Blackboard.GetVariable<SpaceTowerComponent>("SpaceTowerForMovement");
        
        var ring = tower.Value?.ParentRing;

        ArgumentNullException.ThrowIfNull(tower);
        ArgumentNullException.ThrowIfNull(ring);

        if (!ring.IsMouseNearby()) return;

        var positionOnRing = ring.MousePositionOnRing();

        _movementIndicator.Transform.WorldPosition2D = positionOnRing;

        if (Input.LeftPressed())
        {
            Fsm.GoToDefault();
        }
    }


    public override void OnEnd()
    {
        _movementIndicator.Enabled = false;
        _movementDrawer.Enabled = false;
    }
 
}