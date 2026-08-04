using System.Drawing;
using Dreambit;
using Dreambit.ECS;
using Dreambit.UI;
using Color = Microsoft.Xna.Framework.Color;

namespace TDGame.Core;

public class GameUiManager : SingletonComponent<GameUiManager>
{
    private UiFrame UiFrame { get; set; }
    public UiLayout Host { get; set; }

    public override void OnAddedToEntity()
    {
        UiFrame = Entity.Create("ui")
            .AttachComponent<UiFrame>()
            .WithLayout("UI/game-ui.xml");

        Host = UiFrame.Layout;
        
        PopulateTowerCards();
    }

    private void PopulateTowerCards()
    {
        var towerCardsStack = Host.GetRequired<UiVerticalStackPanel>("tower-cards-stack");
        
        var towerDefinitions = SpaceDefenseManager.Instance.GetSpaceTowerDefinitions();

        foreach (var towerDefinition in towerDefinitions)
        {
            var cardComponent = UiFrame.CreateComponent("UI/components/tower-card.xml", towerDefinition.Id);
            
            var portrait = Host.GetRequired<UiTexture>(towerDefinition.Id, "portrait");
            portrait.SpritePath = towerDefinition.PlacementSprite.AssetName;
        }
    }
}
