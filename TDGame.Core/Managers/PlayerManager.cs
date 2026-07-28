using Dreambit;
using Dreambit.ECS;
using Microsoft.Xna.Framework;

namespace TDGame.Core.Managers;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    public Entity PlacementIndicator;

    public override void OnCreated()
    {
        PlacementIndicator = Scene.CreateEntity("placement_indicator");
        var placementAnim = PlacementIndicator.AttachComponent<SpriteAnimator>();

        placementAnim.AnimationPath = "animations/railgun/still_anim";
        PlacementIndicator.GetComponent<SpriteDrawer>().WithOpacity(0.5f);
    }

    public override void OnUpdate()
    {
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        var mousePos = Scene.MainCamera.ScreenToWorld(Input.GetMousePosition());
        var planetPosition = SpaceDefenseManager.Instance.PlanetEntity.Transform.WorldPosToVec2;

        float distanceFromPlanet = 1.5f;
        var directionToMouse = Vector2.Normalize(mousePos - planetPosition);
        
        var finalPos = planetPosition + directionToMouse * distanceFromPlanet;

        PlacementIndicator.Transform.Position = finalPos.ToVector3();
    }
}