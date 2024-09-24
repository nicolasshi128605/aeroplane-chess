using DefaultNamespace.Enums;
using Managers;

namespace CardEffect
{
    public class MoveNextGreen : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            currentTile.MovePlayerToNextTile(TileType.Green, Global.Player);
        }
    }
}