using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Managers;

namespace TDGame.Core;

public class SpaceDefenseComponent : Component<SpaceDefenseComponent>
{
    public float Range { get; set; } = 2.0f;
    public float AttacksPerSecond { get; set; } = 1.0f;
    public bool IsAutomatic { get; set; } = true;
    
    public ITargetingBehavior TargetingBehavior = null;
    private TargetingMode _targetingMode = TargetingMode.Nearest;
    
    public TargetingMode TargetingMode
    {
        get => _targetingMode;
        set
        {
            _targetingMode = value;
            
            switch(_targetingMode)
            {
                case TargetingMode.Nearest:
                    TargetingBehavior = new TargetNearest();
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

    public SpaceEnemyComponent Target = null;


    private float _attackTimer = 0.0f;
    public override void OnUpdate()
    {
        if (TargetingBehavior is null) return;

        _attackTimer -= Time.DeltaTime;
        if (_attackTimer <= 0.0f)
        {
            _attackTimer = AttacksPerSecond;

            Target = TargetingBehavior.SelectTarget(this);

            if (Target != null)
                OnAttack();
        }
    }

    protected virtual void OnAttack()
    {
        FaceToTarget();
        EnemyManager.Instance.DestroyEnemy(Target);
    }

    private void FaceToTarget()
    {
        var pos = Transform.WorldPosition.ToVector2();
        var enemyPos = Target.Transform.WorldPosition.ToVector2();

        var angle = Mathf.AngleBetweenVectors(pos, enemyPos);

        Transform.Rotation.Z = angle;
    }

    public override void OnDebugDraw()
    {
        
    }
}

public enum TargetingMode
{
    Nearest,
    Farthest,
    Strongest,
    Weakest
}

