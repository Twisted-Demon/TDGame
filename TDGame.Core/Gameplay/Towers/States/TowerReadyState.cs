using System;
using Dreambit;

namespace TDGame.Core;

public class TowerReadyState : State
{
    private new SpaceTowerBlackboard Blackboard => (SpaceTowerBlackboard)base.Blackboard;
    private SpaceTowerComponent _towerComponent;

    private float _attackTimer;

    public override void OnInitialize()
    {
        _towerComponent = Fsm.Entity.GetComponent<SpaceTowerComponent>();
        ArgumentNullException.ThrowIfNull(_towerComponent);
    }

    public override void OnExecute()
    {
        if (_towerComponent.TargetingBehavior is null) return;

        _attackTimer -= Time.DeltaTime;

        if (_attackTimer <= 0.0f)
        {
            _attackTimer += Blackboard.TowerDefinition.BaseAttackRate;
            
            _towerComponent.Attack();
        }
    }
}