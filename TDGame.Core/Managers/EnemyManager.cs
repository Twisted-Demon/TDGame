using System;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Managers;

public class EnemyManager : SingletonComponent<EnemyManager>
{
    public List<SpaceEnemyComponent> ActiveEnemies { get; set; } = [];

    private EntityBlueprint _spaceDiverBp;

    private float minRadiusSpawn = 15.0f;
    private float maxRadiusSpawn = 22.0f;

    private float minAngleDegreesSpawn = Mathf.Epsilon;
    private float maxAngleDegreesSpawn = Mathf.Epsilon;

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
    
    private float _spawnTimer;
    private float _spawnInterval = 1f;

    public override void OnUpdate()
    {
        _spawnTimer -= Time.DeltaTime;

        _spawnInterval -= Time.DeltaTime / (60f * 10f);

        if (_spawnTimer > 0f)
            return;

        _spawnTimer = _spawnInterval;

        Vector2 spawnPosition = GenerateRandomSpawnPoint();
        SpawnSpaceDiver(spawnPosition.ToVector3());
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

    private Vector2 GenerateRandomSpawnPoint()
    {
        float radius = Random.Shared.NextFloat(
            minRadiusSpawn,
            maxRadiusSpawn
        );

        float angleDegrees = Random.Shared.NextFloat(
            minAngleDegreesSpawn,
            maxAngleDegreesSpawn
        );

        float angleRadians = Mathf.Radians(angleDegrees);

        return PolarToPosition(
            Transform.WorldPosition2D,
            radius,
            angleRadians
        );
    }

    private Vector2 PolarToPosition(Vector2 center, float radius, float angleRadians)
    {
        float x = Mathf.Cos(angleRadians) * radius;
        float y = Mathf.Sin(angleRadians) * radius;

        return center + new Vector2(x, y);
    }
}