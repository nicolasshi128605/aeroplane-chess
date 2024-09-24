using DefaultNamespace.Enums;
using Managers;

namespace CardEffect
{
    public class MoveNextRed : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            currentTile.MovePlayerToNextTile(TileType.Red, Global.Player);
        }
    }
}