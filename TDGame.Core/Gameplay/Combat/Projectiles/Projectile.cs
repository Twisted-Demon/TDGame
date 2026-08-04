using System.Collections;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[Require(typeof(SpriteAnimator))]
public class Projectile : Component
{
    [FromRequired]
    public SpriteAnimator Animator { get; set; }
    
    public float InitialVelocity { get; set; }
    public float LifeTime { get; set; } = 6f;
    
    public override void OnAddedToEntity()
    {
        _lifeTime = LifeTime;
        CoroutineService.StartCoroutine(LifeTimeTick());
    }
    
    public override void OnUpdate()
    {
        Seek();
    }

    protected virtual void Seek()
    {
        Transform.Position += Transform.Forward * InitialVelocity * Time.DeltaTime;
    }

    private float _lifeTime;
    private IEnumerator LifeTimeTick()
    {
        while (_lifeTime > 0)
        {
            _lifeTime -= Time.DeltaTime;

            yield return null;
        }

        Entity.Destroy(Entity);
    }
}
