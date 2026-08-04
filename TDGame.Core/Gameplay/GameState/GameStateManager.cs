using System;
using Dreambit;
using Dreambit.ECS;

namespace TDGame.Core;

[Require(typeof(FSM))]
public class GameStateManager : SingletonComponent<GameStateManager>
{
    [FromRequired]
    private FSM _fsm;
    private GameStateBlackboard _blackboard;

    public override void OnCreated()
    {
        _blackboard = _fsm.SetBlackboard<GameStateBlackboard>();
        
        _fsm.Register(
            typeof(BrowseInteractionState), 
            typeof(PlaceTowerState));

        _fsm.SetDefaultState<BrowseInteractionState>();
        _fsm.GoToDefault();
    }

    public void PlayerClickedTowerList(SpaceTowerDefinition towerDefinition)
    {
        _blackboard.PlacementTowerDefinition = towerDefinition;
    }
}