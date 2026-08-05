using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[Require(typeof(FSM))]
public class SpaceTowerComponent : Component
{
    public new TDGameScene Scene => (TDGameScene)base.Scene;
    
    [FromRequired]
    public FSM Fsm { get; set; }
    public SpaceTowerBlackboard Blackboard;
    public OrbitalRing ParentRing { get; set; }

    protected SpaceEnemyComponent Target { get; set; }

    public TargetingMode TargetingMode { get; set; } = TargetingMode.Nearest;

    public ITargetingBehavior TargetingBehavior
    {
        get
        {
            switch (TargetingMode)
            {
                case TargetingMode.Nearest:
                    return new TargetNearest();
                    break;
                case TargetingMode.Farthest:
                    break;
                case TargetingMode.Strongest:
                    break;
                case TargetingMode.Weakest:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }

    public override void OnCreated()
    {
        SetUpFsm();
    }

    public override void OnAddedToEntity()
    {
        OnSpawnReady();

        SpaceTowersManager.Instance.MarkTowerReady(this);
    }

    public override void OnDestroyed()
    {
        if (!IsNull(SpaceTowersManager.Instance))
            SpaceTowersManager.Instance.UnregisterTower(this);
    }

    private void SetUpFsm()
    {
        Blackboard = Fsm.SetBlackboard<SpaceTowerBlackboard>();
        
        Fsm.Register(
            typeof(TowerReadyState),
            typeof(TowerRepositioningState));

        Fsm.SetDefaultState<TowerReadyState>();
        Fsm.GoToDefault();
    }

    public SpaceTowerComponent WithDefinition(SpaceTowerDefinition definition)
    {
        Blackboard.TowerDefinition = definition;
        
        return this;
    }

    protected void FaceTarget()
    {
        if (Target is null) return;

        var enemyPos = Target.Transform.WorldPosition2D;
        Transform.LookAt2D(enemyPos);
    }
    

    public virtual void Attack()
    {
    }

    protected void OnSpawnReady()
    {
    }
}