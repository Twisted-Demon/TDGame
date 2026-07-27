using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Framework.Content.Pipeline.Builder;

if (args.Length == 0)
{
    Console.Error.WriteLine(
        "No content platform was supplied. " +
        "Build a host project such as TDGame.VK instead.");

    return -1;
}

var builder = new Builder();
builder.Run(args);

return builder.FailedToBuild > 0 ? -1 : 0;

public sealed class Builder : ContentBuilder
{
    public override IContentCollection GetContentCollection()
    {
        var content = new ContentCollection();

        content.Include<WildcardRule>("*.fx");

        content.IncludeCopy<WildcardRule>("*.ttf");
        content.IncludeCopy<WildcardRule>("*.pak");

        content.Exclude<WildcardRule>("*.xnb");

        return content;
    }
}
