using Dreambit.Events;

namespace TDGame.Core;

public class TowerSelectedEvent : DreambitEvent<TowerSelectedEventArgs>
{
    private TowerSelectedEvent()
    {
        
    }

    public static TowerSelectedEvent Instance { get; } = new();
}

public class TowerSelectedEventArgs : DreambitEventArgs
{
    public required SpaceTowerComponent SelectedTower { get; set; }
}