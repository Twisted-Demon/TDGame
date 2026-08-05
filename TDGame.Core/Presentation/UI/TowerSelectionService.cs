using System;
using Dreambit.ECS;
using Dreambit.Events;
using Dreambit.UI;

namespace TDGame.Core;

public class TowerSelectionService : SingletonComponent<TowerSelectionService>
{
    public Action<SpaceTowerDefinition> TowerSelected;

    public override void OnAddedToEntity()
    {
        PopulateTowerCards();
    }

    public override void OnRemovedFromEntity()
    {
        TowerSelected = null;
    }

    private void PopulateTowerCards()
    {
        var uiHost = GameUiManager.Instance.Host;
        var uiFrame = GameUiManager.Instance.UiFrame;
        
        var towerCardsListBox =
            uiHost.GetRequired<UiListBox>("tower-cards-stack");

        var towerDefinitions =
            SpaceTowersManager.Instance.GetSpaceTowerDefinitions();

        towerCardsListBox.ClearItems();

        towerCardsListBox.SetItems(towerDefinitions, towerDefinition =>
        {
            var cardComponent = uiFrame.CreateComponent("UI/components/tower-card.xml", towerDefinition.Id);

            var portrait = cardComponent.GetRequired<UiTexture>(towerDefinition.Id, "portrait");
            portrait.Sprite = towerDefinition.PlacementSprite;

            cardComponent.GetRequired<UiButton>(towerDefinition.Id, "card")
                .Clicked += uiButton =>
            {
                var eventArgs = new PlaceTowerStartedEventArgs
                {
                    TowerDefinition = towerDefinition
                };

                EventBus.Instance.Invoke(PlaceTowerStartedEvent.Instance, eventArgs);
            };

            return cardComponent;
        });
    }
}