using Dreambit.ECS;
using Dreambit.UI;

namespace TDGame.Core;

[Require(typeof(TowerSelectionService))]
public class GameUiManager : SingletonComponent<GameUiManager>
{
    public UiFrame UiFrame { get; set; }
    public UiLayout Host { get; set; }

    public override void OnCreated()
    {
        UiFrame = Entity.Create("ui")
            .AttachComponent<UiFrame>()
            .WithLayout("UI/game-ui.xml");

        Host = UiFrame.Layout;
    }
}