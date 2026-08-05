using System;
using Dreambit;
using Dreambit.Events;

namespace TDGame.Core;

public class BrowseInteractionState : State
{
    private ILogger _logger = new Logger<BrowseInteractionState>();
    private new GameStateBlackboard Blackboard => (GameStateBlackboard)Fsm.Blackboard;

    public override void OnExecute()
    {
        //CheckForTowerSelection();
    }

    private void CheckForTowerSelection()
    {
        if (!Input.LeftPressed()) return;
        
        var mousePos = Input.GetMousePosition();
        var mouseWorldPos = Scene.Instance.MainCamera.ScreenToWorld(mousePos);

        if (!PhysicsSystem.Instance.PointCastByTag(
                mouseWorldPos, 
                out var hitTest, 
                ["tower"])) return;
        
        
        var tower = hitTest.First.Entity.GetComponent<SpaceTowerComponent>();

        if (tower is null) return;

        var eventArgs = new TowerSelectedEventArgs
        {
            SelectedTower = tower
        };
        EventBus.Instance.Invoke(TowerSelectedEvent.Instance, eventArgs);
        
        _logger.Info($"{tower.Entity.Name} selected");
    }
}