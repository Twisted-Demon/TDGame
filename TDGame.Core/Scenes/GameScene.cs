using Dreambit;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Scenes;

public class GameScene : Scene<GameScene>
{
    protected override void OnInitialize()
    {
        Window.SetSize(1280, 720);

        BackgroundColor = Color.Blue;
    }
}