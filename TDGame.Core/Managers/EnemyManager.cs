using System;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Managers;

public class EnemyManager : SingletonComponent<EnemyManager>
{
    private readonly Dictionary<string, EnemyDefinition> _definitions =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly HashSet<SpaceEnemyComponent> _pendingEnemies = [];
    private readonly HashSet<SpaceEnemyComponent> _activeEnemies = [];

    public HashSet<SpaceEnemyComponent> ActiveEnemies => _activeEnemies;
    
    public bool HasLivingOrPendingEnemies => _activeEnemies.Count > 0 ||
                                             _pendingEnemies.Count > 0;

    public override void OnCreated()
    {
        RegisterDefinition(new EnemyDefinition
        {
            Id = "space_diver",
            Blueprint = Resources.LoadAsset<EntityBlueprint>("blueprints/space_diver_bp"),
            
            ThreatCost = 1,
            FirstAvailableWave = 1
        });
    }

    public SpaceEnemyComponent SpawnEnemy(string enemyId, Vector3 spawnPosition)
    {
        if (!_definitions.TryGetValue(enemyId, out var definition))
        {
            throw new ArgumentException(
                $"No enemy definition is registered with ID '{enemyId}'.",
                nameof(enemyId));
        }

        var entity = Scene.CreateEntity(definition.Blueprint, createAt: spawnPosition);

        var enemy = entity.GetComponent<SpaceEnemyComponent>();

        if (enemy is null)
        {
            Entity.Destroy(entity);
            
            throw new InvalidOperationException(
                $"Enemy blueprint '{enemyId}' does not contain a " +
                $"{nameof(SpaceEnemyComponent)}.");
        }

        _pendingEnemies.Add(enemy);
        
        Logger.Info($"Enemy created: {enemyId} at {spawnPosition}");

        return enemy;
    }

    public void MarkEnemyReady(SpaceEnemyComponent enemy)
    {
        if (enemy is null)
            return;

        _pendingEnemies.Remove(enemy);
        _activeEnemies.Add(enemy);
    }
    
    public void UnregisterEnemy(SpaceEnemyComponent enemy)
    {
        if (enemy is null)
            return;

        _pendingEnemies.Remove(enemy);
        _activeEnemies.Remove(enemy);
    }
    
    public void DestroyEnemy(SpaceEnemyComponent enemy)
    {
        if (enemy is null || Entity.IsDestroyed(enemy.Entity))
            return;

        var enemyName = enemy.Entity.Name;

        // Remove immediately so targeting systems cannot select it.
        UnregisterEnemy(enemy);

        Entity.Destroy(enemy.Entity);

        Logger.Info($"Enemy destroyed: {enemyName}");
    }

    private void RegisterDefinition(EnemyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (!_definitions.TryAdd(definition.Id, definition))
        {
            throw new InvalidOperationException(
                $"Enemy definition '{definition.Id}' is already registered.");
        }
    }
}