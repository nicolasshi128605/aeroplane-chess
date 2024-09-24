using DefaultNamespace.Enums;
using Managers;

namespace CardEffect
{
    public class MoveNextBlue : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            currentTile.MovePlayerToNextTile(TileType.Blue, Global.Player);
        }
    }
}