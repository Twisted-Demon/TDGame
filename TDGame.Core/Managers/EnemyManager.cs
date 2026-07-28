using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Managers;

public class EnemyManager : SingletonComponent<EnemyManager>
{
    public List<SpaceEnemyComponent> ActiveEnemies { get; set; } = [];

    private EntityBlueprint _spaceDiverBp;

    public override void OnCreated()
    {
        _spaceDiverBp = Resources.LoadAsset<EntityBlueprint>("blueprints/space_diver_bp");
    }

    public void SpawnSpaceDiver(Vector3 spawnPosition)
    {
        var enemy = Scene.CreateEntity(_spaceDiverBp, createAt: spawnPosition)
            .GetComponent<SpaceEnemyComponent>();

        ActiveEnemies.Add(enemy);
        
        Logger.Info($"Space Diver created: {spawnPosition}");
    }

    public void DestroyEnemy(SpaceEnemyComponent enemyToDestroy)
    {
        ActiveEnemies.Remove(enemyToDestroy);

        Entity.Destroy(enemyToDestroy.Entity);
        
        Logger.Info($"Enemy Destroyed: {enemyToDestroy.Entity.Name}");
    }

    //todo: use physics query to get closest enemies, then find the closest
    public SpaceEnemyComponent FindClosestEnemy(Vector3 fromPosition)
    {
        float closestDistanceSquared = float.MaxValue;
        SpaceEnemyComponent closestEnemy = null;

        foreach (var enemy in ActiveEnemies)
        {
            float distanceSquared = Vector3.DistanceSquared(
                fromPosition, enemy.Transform.WorldPosition);

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}