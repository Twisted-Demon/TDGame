using System;
using Dreambit;

namespace TDGame.Core;

public class SpaceTowerDefinitionLoader : AssetLoaderBase
{
    public override string Extension { get; } = ".jsonb";
    public override bool AddToDisposableList { get; } = true;
    public override Type TargetType { get; } = typeof(SpaceTowerDefinition);
    public override object Load(string assetName, string pakName, bool usePak, string contentDirectory)
    {
        using var s = GetStream(GetPath(assetName), pakName, usePak, contentDirectory);

        var definition = JsnbLoader.Deserialize<SpaceTowerDefinition>(s);
        definition.AssetName = assetName;

        return definition;
    }
}
