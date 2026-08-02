using System;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

[Require(typeof(ParticleTest))]
public class SpaceDefenseManager : SingletonComponent<SpaceDefenseManager>
{
    private readonly Dictionary<string, SpaceTowerDefinition> _definitions =
        new(StringComparer.OrdinalIgnoreCase);

    public Entity PlanetEntity { get; set; }

    private readonly HashSet<SpaceTowerComponent> _pendingTowers = [];
    private readonly HashSet<SpaceTowerComponent> _activeTowers = [];

    public HashSet<SpaceTowerComponent> ActiveTowers => _activeTowers;

    public bool HasLivingOrPendingTowers => _activeTowers.Count > 0 ||  
                                            _pendingTowers.Count > 0;
    
    public override void OnCreated()
    {
        CreatePlanet();
        RegisterTowerDefinitions();
    }


    public void CreatePlanet()
    {
        var planetBp = Resources.LoadAsset<EntityBlueprint>(
            "gameplay/planets/terran-planet/terran-planet.blueprint");

        PlanetEntity = Scene.CreateEntity(planetBp);
        Scene.MainCamera.ForcePosition(PlanetEntity.Transform.Position);
        Logger.Info($"Created planet {PlanetEntity.Name} at {PlanetEntity.Transform.WorldPosition}");
    }

    public SpaceTowerComponent SpawnTower(string towerId, Vector3 spawnPosition)
    {
        if (!_definitions.TryGetValue(towerId, out var definition))
        {
            throw new ArgumentException(
                $"No space tower definition is registered with ID '{towerId}'.",
                nameof(towerId));
        }
        
        var entity = Scene.CreateEntity(definition.Blueprint, createAt: spawnPosition);

        var tower = entity.GetComponent<SpaceTowerComponent>();
        
        if(tower is null)
        {
            Entity.Destroy(entity);
            
            throw new InvalidOperationException(
                $"Tower blueprint '{towerId}' does not contain a " +
                $"{nameof(SpaceTowerComponent)}.");
        }
        
        tower.WithDefinition(definition);
        _pendingTowers.Add(tower);

        Logger.Info($"Spawned tower {towerId} at {spawnPosition}");

        return tower;
    }

    public void MarkTowerReady(SpaceTowerComponent tower)
    {
        if (tower is null) return;

        _pendingTowers.Remove(tower);
        _activeTowers.Add(tower);
    }

    public void UnregisterTower(SpaceTowerComponent tower)
    {
        if(tower is null) return;
        
        _pendingTowers.Remove(tower);
        _activeTowers.Remove(tower);
    }

    public void DestroyTower(SpaceTowerComponent tower)
    {
        if (tower is null || Entity.IsDestroyed(tower.Entity))
            return;
        
        var towerName = tower.Entity.Name;
        
        UnregisterTower(tower);
        
        Entity.Destroy(tower.Entity);
        
        Logger.Info($"Destroyed tower {towerName}");
    }

    public SpaceTowerDefinition GetSpaceTowerDefinition(string towerId)
    {
        return _definitions.GetValueOrDefault(towerId);
    }

    private void RegisterDefinition(SpaceTowerDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        
        if (!_definitions.TryAdd(definition.Id, definition))
        {
            throw new InvalidOperationException(
                $"Space Tower definition '{definition.Id}' is already registered.");
        }
    }

    private void RegisterDefinition(string definitionPath)
    {
        var definition = Resources.LoadAsset<SpaceTowerDefinition>(definitionPath);
        RegisterDefinition(definition); // throws if definition is null
    }

    private void RegisterTowerDefinitions()
    {
        //Railgun definitions
        RegisterDefinition(
            "gameplay/towers/railgun/railgun.tower-definition");
        
        //missile launcher definitions
        RegisterDefinition(
            "gameplay/towers/missile-launcher/missile-launcher.tower-definition");
    }
}
