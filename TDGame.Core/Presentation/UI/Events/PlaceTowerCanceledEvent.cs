using Dreambit.Events;

namespace TDGame.Core;

public class PlaceTowerCanceledEvent : DreambitEvent
{
    private PlaceTowerCanceledEvent(){}

    public static PlaceTowerCanceledEvent Instance { get; } = new();
}