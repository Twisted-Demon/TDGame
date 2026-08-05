using System;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Dreambit.Events;

namespace TDGame.Core;

[Require(typeof(FSM))]
public class GameStateManager : SingletonComponent<GameStateManager>
{
    private GameStateBlackboard _blackboard;

    [FromRequired] private FSM Fsm { get; set; }

    private List<IDisposable> _eventSubscriptions = [];


    public override void OnCreated()
    {
        RegisterEventsSubscription();
        SetUpFsm();
    }

    private void SetUpFsm()
    {
        _blackboard = Fsm.SetBlackboard<GameStateBlackboard>();

        Fsm.Register(
            typeof(BrowseInteractionState),
            typeof(PlaceTowerState),
            typeof(RepositionTowerState));

        
        Fsm.AddTransition<BrowseInteractionState, PlaceTowerState>(fsm =>
        {
            var eventOccurred = fsm.TryConsumeEvent<PlaceTowerStartedEvent>();
            var hasTower = _blackboard.TowerDefinitionForPlacement.Value != null;
            
            return eventOccurred && hasTower;
        });
        

        Fsm.SetDefaultState<BrowseInteractionState>();
        Fsm.GoToDefault();
    }
    
    private void RegisterEventsSubscription()
    {
        _eventSubscriptions.Add(EventBus.Instance.Subscribe(
            PlaceTowerStartedEvent.Instance, args =>
            {
                _blackboard.TowerDefinitionForPlacement.Value = args.TowerDefinition;
                Fsm.Trigger<PlaceTowerStartedEvent>();
            }));
    }

    public override void OnDestroyed()
    {
        UnregisterEventsSubscription();
    }
    
    private void UnregisterEventsSubscription()
    {
        foreach (var subscription in _eventSubscriptions)
        {
            subscription?.Dispose();
        }
        
        _eventSubscriptions.Clear();
        _eventSubscriptions = null;
    }
}