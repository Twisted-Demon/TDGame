using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class MissileLauncherComponent : SpaceTowerComponent
{
    public new TDGameScene Scene => base.Scene;
    public Entity Muzzle { get; set; }
    public float ConeAngleDegrees { get; set; } = 60f;

    private EntityBlueprint ProjectileBlueprint =>
        Resources.LoadAsset<EntityBlueprint>(
            "gameplay/projectiles/basic-rocket/basic-rocket.blueprint");
    

    public override void Attack()
    {
        var definition = Blackboard.TowerDefinition;
        
        Target = TargetingBehavior.SelectTarget(Transform, definition.BaseRange, ["enemy"]);

        if (Target is null) return;

        FaceTarget();

        var projectile = Entity.Create(ProjectileBlueprint, createAt: Transform.Position)
            .GetComponent<HomingProjectile>();

        projectile.Target = Target.Entity;
        projectile.Transform.Rotation2D = Transform.Rotation2D;
        projectile.LifeTime = 10f;
        projectile.InitialVelocity = 7.0f;

        GameAudioManager.Instance.Play(definition.WeaponSoundCue);
    }

    private static bool IsInsideCone(
        Vector2 towerPosition,
        Vector2 forward,
        Vector2 enemyPosition,
        float coneAngleDegrees)
    {
        var toEnemy = enemyPosition - towerPosition;

        if (toEnemy.LengthSquared() <= Mathf.Epsilon)
            return true;

        toEnemy.Normalize();
        forward.Normalize();

        var halfAngleRadians
            = MathHelper.ToRadians(coneAngleDegrees * 0.5f);

        var minimumDot = Mathf.Cos(halfAngleRadians);
        var alignment = Vector2.Dot(forward, toEnemy);

        return alignment >= minimumDot;
    }
}