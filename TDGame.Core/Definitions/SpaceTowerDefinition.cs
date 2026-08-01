using Dreambit;

namespace TDGame.Core;

public class SpaceTowerDefinition : DreambitAsset
{
    public required string Id { get; init; }
    public required EntityBlueprint Blueprint { get; init; }
    public required Sprite PlacementSprite { get; init; }
    public required SoundCue WeaponSoundCue { get; init; }
}