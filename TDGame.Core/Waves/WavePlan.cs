using System.Collections.Generic;

namespace TDGame.Core;

public class WavePlan
{
    public required string Name { get; init; }
    public required IReadOnlyList<SpawnGroup> Groups { get; init; }
    public float IntermissionAfterWave { get; init; } = 5f;
}