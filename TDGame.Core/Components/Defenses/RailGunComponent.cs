using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class RailGunComponent : SpaceDefenseComponent
{
    public new TDGameScene Scene { get; set; }
    public new ILogger Logger  = new Logger<SpaceDefenseComponent>();
    public Vector2 WeaponOriginOffset { get; set; }

    public override void OnCreated()
    {
        Scene = Dreambit.Core.Instance.CurrentScene as TDGameScene;
        
        if(Scene == null)
            throw new ArgumentNullException(nameof(Scene));
    }
}