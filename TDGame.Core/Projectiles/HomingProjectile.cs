using Dreambit;
using Dreambit.ECS;

namespace TDGame.Core;

public class HomingProjectile : Projectile
{
    public Transform Target { get; set; }

    protected override void Seek()
    {
        if (Target is not null)
        {
            var targetPosition = Target.WorldPosToVec2;
            var position = Transform.WorldPosToVec2;
        
            var angle = Mathf.AngleBetweenVectors(position, targetPosition);

            Transform.Rotation.Z = angle;
        }

        Transform.Position += Transform.Forward * InitialVelocity * Time.DeltaTime;
    }
}