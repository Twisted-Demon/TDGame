using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class RailGunComponent : SpaceTowerComponent, ICanLog<RailGunComponent>
{
    public Entity Muzzle { get; set; }
    public ILogger Logger { get; } = new Logger<SpaceTowerComponent>();
    

    public override void Attack()
    {
        Target = TargetingBehavior.SelectTarget(Transform, Blackboard.TowerDefinition.BaseRange, ["enemy"]);

        if (Target is null) return;

        FaceTarget();

        var start = Muzzle.Transform.WorldPosition;
        var end = Target.Transform.WorldPosition;

        HitScanLine.Create(start, end, Color.White, 0.5f);

        GameAudioManager.Instance.Play(Blackboard.TowerDefinition.WeaponSoundCue);

        EnemyManager.Instance.DestroyEnemy(Target);
    }
}