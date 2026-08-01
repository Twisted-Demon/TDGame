using Dreambit;

namespace TDGame.Core;

public class EnemyDefinition
{
    public required string Id { get; init; }
    public required EntityBlueprint Blueprint { get; init; }
    public int ThreatCost { get; init; } = 1;
    public int FirstAvailableWave { get; init; } = 1;
}