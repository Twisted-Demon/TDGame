using System;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework.Input;

namespace TDGame.Core;

public abstract class SpaceEnemyComponent : Component
{
    public new TDGameScene Scene => (TDGameScene)base.Scene;

    public Entity Planet { get; private set; }

    public float BaseHealth { get; set; } = 1f;
    public float BaseVelocity { get; set; } = 3f;
    
    public float CurrentHealth { get; internal set; }
    public float MovementSpeed { get; internal set; }
    
    public EnemyDefinition EnemyDefinition { get; internal set; }

    public override void OnCreated()
    {
        Planet = SpaceDefenseManager.Instance.PlanetEntity;

        if (Planet is null)
            throw new InvalidOperationException(
                "The planet must exist before enemies are spawned");
    }

    public override void OnAddedToEntity()
    {
        CurrentHealth = BaseHealth;
        MovementSpeed = BaseVelocity;
        
        OnSpawnReady();
        
        EnemyManager.Instance.MarkEnemyReady(this);
    }

    public override void OnDestroyed()
    {
        if (!Component.IsNull(EnemyManager.Instance))
            EnemyManager.Instance.UnregisterEnemy(this);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;
        
        CurrentHealth -= damage;


        if (CurrentHealth <= 0f)
            EnemyManager.Instance.DestroyEnemy(this);
    }

    protected virtual void OnSpawnReady()
    {
        
    }
    
    
}
