namespace TDGame.Core;

public class SpawnGroup
{
    public required string EnemyId { get; init; }
    public required int Count { get; init; }
    
    public required SpawnSector Sector { get; init; }

    public float SpawnInterval { get; init; } = 1f;
    public float DelayBeforeGroup { get; init; } = 0f;
}
