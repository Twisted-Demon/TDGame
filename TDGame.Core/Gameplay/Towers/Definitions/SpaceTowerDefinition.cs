using Dreambit;

namespace TDGame.Core;

public class SpaceTowerDefinition : DreambitAsset
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }
    public required int BuildCost { get; init; }
    public required EntityBlueprint Blueprint { get; init; }
    public required Sprite PlacementSprite { get; init; }
    public required SoundCue WeaponSoundCue { get; init; }
    
    public required bool IsAutomatic { get; init; }
    public required float BaseAttackRate { get; init; }
    public required float BaseRange { get; init; }
}