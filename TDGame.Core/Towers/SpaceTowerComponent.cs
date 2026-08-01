using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class SpaceTowerComponent : Component
{
    public new TDGameScene Scene => (TDGameScene)base.Scene;
    
    public SpaceTowerDefinition Definition { get; set; }
    
    public float BaseRange { get; init; } = 3.5f;
    public float BaseAttackRate { get; init; } = 1.0f;

    public float CurrentRange => BaseRange;
    public float CurrentAttackRate => BaseAttackRate;
    
    public bool IsAutomatic { get; init; }
    
    protected SpaceEnemyComponent Target { get; set; }
    
    public TargetingMode TargetingMode { get; set; } =  TargetingMode.Nearest;
    public ITargetingBehavior TargetingBehavior
    {
        get
        {
            switch(TargetingMode)
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
    
    private Vector2 _attackOrigin =  Vector2.Zero;
    public Vector2 AttackOrigin
    {
        get => _attackOrigin;
        set
        {
            var x = value.X / Scene.MainCamera.PixelsPerUnit;
            var y = value.Y / Scene.MainCamera.PixelsPerUnit;
            
            _attackOrigin = new Vector2(x, y);
        }
    }
    

    public override void OnAddedToEntity()
    {
        OnSpawnReady();

        SpaceDefenseManager.Instance.MarkTowerReady(this);
    }
    
    public override void OnUpdate()
    {
        UpdateWeapon();
    }

    public override void OnDestroyed()
    {
        if (!Component.IsNull(SpaceDefenseManager.Instance))
            SpaceDefenseManager.Instance.UnregisterTower(this);
    }

    public SpaceTowerComponent WithDefinition(SpaceTowerDefinition definition)
    {
        Definition = definition;
        return this;
    }

    protected void FaceTarget()
    {
        if (Target is null) return;

        var enemyPos = Target.Transform.WorldPosition2D;
        Transform.LookAt2D(enemyPos);
    }

    private float _attackTimer = 0.0f;
    private void UpdateWeapon()
    {
        if (TargetingBehavior is null) return;

        _attackTimer -= Time.DeltaTime;

        if (_attackTimer <= 0.0f)
        {
            _attackTimer += CurrentAttackRate;
            
            OnAttack();
        }
    }

    protected virtual void OnAttack()
    {
        
    }
    
    protected void OnSpawnReady()
    {
        
    }
}