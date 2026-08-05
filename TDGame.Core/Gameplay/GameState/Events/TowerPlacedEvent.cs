using Dreambit.Events;

namespace TDGame.Core;

public class TowerPlacedEvent : DreambitEvent<TowerPlacedEventArgs>
{
    private TowerPlacedEvent()
    {
        
    }

    public static TowerPlacedEvent Instance { get; } = new();
}

public sealed class TowerPlacedEventArgs : DreambitEventArgs
{
    public required SpaceTowerComponent Tower { get; init; }
}