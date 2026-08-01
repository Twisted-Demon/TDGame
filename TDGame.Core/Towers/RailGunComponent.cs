using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;
using TDGame.Core.Projectiles;

namespace TDGame.Core;

public class RailGunComponent : SpaceTowerComponent, ICanLog<RailGunComponent>
{
    public new TDGameScene Scene { get; set; }
    public ILogger Logger { get; } = new Logger<SpaceTowerComponent>();
    public Entity Muzzle { get; set; }

    public override void OnCreated()
    {
        Scene = Dreambit.Core.Instance.CurrentScene as TDGameScene;
        
        if(Scene == null)
            throw new ArgumentNullException(nameof(Scene));
    }

    protected override void OnAttack()
    {
        Target = TargetingBehavior.SelectTarget(Transform, CurrentRange, ["enemy"]);

        if (Target is null) return;
        
        FaceTarget();
        
        var start = Muzzle.Transform.WorldPosition;
        var end = Target.Transform.WorldPosition;

        HitScanLine.Create(start, end, Color.White, 0.5f);
        
        GameAudioManager.Instance.Play(Definition.WeaponSoundCue);
        
        EnemyManager.Instance.DestroyEnemy(Target);
    }
}