using System;
using System.Collections;
using System.Collections.Generic;
using Dreambit;
using Dreambit.ECS;
using Dreambit.UI;

namespace TDGame.Core;

public sealed class WaveDirectorComponent :
    SingletonComponent<WaveDirectorComponent>
{
    private enum WaveState
    {
        Intermission,
        Spawning,
        WaitingForEnemies,
        Completed
    }

    private readonly List<WavePlan> _waves = [];

    private WaveState _state;

    private int _waveIndex = -1;
    private int _remainingGroupsInWave;

    private bool _intermissionCoroutineRunning;

    public int CurrentWaveNumber => _waveIndex + 1;

    private bool HasCurrentWave =>
        _waveIndex >= 0 &&
        _waveIndex < _waves.Count;

    private WavePlan CurrentWave
    {
        get
        {
            if (!HasCurrentWave)
            {
                throw new InvalidOperationException(
                    $"There is no current wave. Wave index: {_waveIndex}, " +
                    $"configured waves: {_waves.Count}.");
            }

            return _waves[_waveIndex];
        }
    }

    private UiFrame _uiPanel;

    public override void OnCreated()
    {
        _waves.Add(AuthoredSpawnWaves.FirstWave);
        _waves.Add(AuthoredSpawnWaves.SecondWave);
        _waves.Add(AuthoredSpawnWaves.ThirdWave);
        _waves.Add(AuthoredSpawnWaves.FourthWave);
        _waves.Add(AuthoredSpawnWaves.FifthWave);
        _waves.Add(AuthoredSpawnWaves.SixthWave);
        _waves.Add(AuthoredSpawnWaves.SeventhWave);
        _waves.Add(AuthoredSpawnWaves.EighthWave);
        _waves.Add(AuthoredSpawnWaves.NinthWave);
        _waves.Add(AuthoredSpawnWaves.TenthWave);

        _uiPanel = Entity.Create("wave-counter")
            .AttachComponent<UiFrame>().WithLayout("UI/tool-bar.xml");

        BeginIntermission(2f);
    }

    public override void OnUpdate()
    {
        switch (_state)
        {
            case WaveState.Intermission:
                // The intermission coroutine advances this state.
                break;

            case WaveState.Spawning:
                UpdateSpawning();
                break;

            case WaveState.WaitingForEnemies:
                UpdateWaitingForEnemies();
                break;

            case WaveState.Completed:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateSpawning()
    {
        if (_remainingGroupsInWave > 0)
            return;

        _state = WaveState.WaitingForEnemies;
    }

    private void UpdateWaitingForEnemies()
    {
        if (EnemyManager.Instance.HasLivingOrPendingEnemies)
            return;

        Logger.Info(
            $"Wave {CurrentWaveNumber} completed: {CurrentWave.Name}");

        if (_waveIndex >= _waves.Count - 1)
        {
            CompleteAllWaves();
            return;
        }

        BeginIntermission(CurrentWave.IntermissionAfterWave);
    }

    private void BeginIntermission(float intermissionLength)
    {
        if (_intermissionCoroutineRunning)
            return;

        _state = WaveState.Intermission;
        _intermissionCoroutineRunning = true;

        CoroutineService.StartCoroutine(
            WaitForIntermissionCoroutine(intermissionLength));
    }

    private IEnumerator WaitForIntermissionCoroutine(
        float intermissionLength)
    {
        if (intermissionLength > 0f)
            yield return new WaitForSeconds(intermissionLength);

        _intermissionCoroutineRunning = false;

        // This protects against a stale coroutine changing the state
        // if the director was completed or otherwise transitioned.
        if (_state != WaveState.Intermission)
            yield break;

        BeginNextWave();
    }

    private void BeginNextWave()
    {
        var nextWaveIndex = _waveIndex + 1;

        if (nextWaveIndex >= _waves.Count)
        {
            CompleteAllWaves();
            return;
        }

        _waveIndex = nextWaveIndex;

        var wave = CurrentWave;

        _remainingGroupsInWave = wave.Groups.Count;
        _state = WaveState.Spawning;

        foreach (var group in wave.Groups)
        {
            CoroutineService.StartCoroutine(
                SpawnGroupCoroutine(group));
        }

        Logger.Info(
            $"Wave {CurrentWaveNumber} started: {wave.Name}");

        _uiPanel.Layout.GetRequired<UiText>("wave-counter")
                .Text = $"Current Wave: {_waveIndex}";
    }

    private IEnumerator SpawnGroupCoroutine(SpawnGroup group)
    {
        if (group.DelayBeforeGroup > 0f)
            yield return new WaitForSeconds(group.DelayBeforeGroup);

        var enemiesRemaining = group.Count;

        while (enemiesRemaining > 0)
        {
            // Stop this group if the wave director is no longer spawning.
            if (_state != WaveState.Spawning)
                yield break;

            SpawnEnemy(group);

            enemiesRemaining--;

            // Do not wait after spawning the final enemy.
            if (enemiesRemaining > 0 && group.SpawnInterval > 0f)
                yield return new WaitForSeconds(group.SpawnInterval);
        }

        _remainingGroupsInWave--;
    }

    private static void SpawnEnemy(SpawnGroup group)
    {
        var planetCenter = SpaceDefenseManager.Instance
            .PlanetEntity
            .Transform
            .WorldPosition2D;

        var position = group.Sector.GeneratePosition(planetCenter);

        EnemyManager.Instance.SpawnEnemy(
            group.EnemyId,
            position.ToVector3());
    }

    private void CompleteAllWaves()
    {
        _state = WaveState.Completed;

        Logger.Info("All configured waves completed.");
    }
}
