using System.Drawing;
using Dreambit;
using Dreambit.ECS;
using Dreambit.UI;
using Color = Microsoft.Xna.Framework.Color;

namespace TDGame.Core;

public class GameUiManager : SingletonComponent<GameUiManager>
{
    private UiFrame _uiFrame;
    public UiLayout _ui;

    public override void OnCreated()
    {
        _uiFrame = Entity.Create("ui")
            .AttachComponent<UiFrame>()
            .WithLayout("UI/game-ui.xml");

        
        var unitList = _uiFrame.Layout.GetRequired<UiListBox>("unit-list");
        unitList.AddItem(CreateUnitButton(
            "gameplay/towers/railgun/railgun-placement.sprite"));
        
        unitList.AddItem(CreateUnitButton(
            "gameplay/towers/missile-launcher/missile-launcher-placement.sprite"));
        
        unitList.SetItems(["tower", "tower"] , CreateUnitButton);
        

    }

    private UiElement CreateUnitButton(string sprite)
    {
        var button = new UiButton();

        button.Width = UiLength.Pixels(48);
        button.Height = UiLength.Pixels(48);
        button.BackgroundTint = Color.White;
        button.HoverTint = ColorExt.FromHex("#34465A");
        button.SelectedTint = ColorExt.FromHex("#376A99");
        button.Background = new SpriteBrush(sprite);

        return button;
    }
}