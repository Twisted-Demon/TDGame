using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core;

[Require(typeof(SpriteDrawer))]
public class SpaceEnemyComponent : Component
{
    [FromRequired]
    private SpriteDrawer Sprite { get; set; }

    public override void OnUpdate()
    {
        Transform.Rotation.Z += Time.DeltaTime;
        
        if(Input.IsKeyPressed(Keys.F7))
            Scene.DebugMode = true;
    }
}