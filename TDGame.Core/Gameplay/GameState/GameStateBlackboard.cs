using Dreambit;

namespace TDGame.Core;

public class GameStateBlackboard : Blackboard
{
    public BlackboardVar<SpaceTowerComponent> SpaceTowerForMovement { get; }
    
    public BlackboardVar<SpaceTowerDefinition> TowerDefinitionForPlacement { get; }

    public GameStateBlackboard()
    {
        TowerDefinitionForPlacement =
            CreateVariable<SpaceTowerDefinition>("TowerDefinitionForPlacement", null);

        SpaceTowerForMovement =
            CreateVariable<SpaceTowerComponent>("SpaceTowerForMovement", null);
    }
}