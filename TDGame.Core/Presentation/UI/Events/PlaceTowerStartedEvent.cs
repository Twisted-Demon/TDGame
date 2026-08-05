using Dreambit.Events;

namespace TDGame.Core;

public class PlaceTowerStartedEvent : DreambitEvent<PlaceTowerStartedEventArgs>
{
    private PlaceTowerStartedEvent()
    {
    }

    public static PlaceTowerStartedEvent Instance { get; } = new();
}

public sealed class PlaceTowerStartedEventArgs : DreambitEventArgs
{
    public required SpaceTowerDefinition TowerDefinition { get; init; }
}