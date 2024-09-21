using DG.Tweening;
using Managers;

namespace CardEffect
{
    public class MoveLeftOne : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            Global.Player.Jump(0.5f);
            if (currentTile.leftTile != null)
            {
                Global.Player.ChangeToLeft();
                Global.Player.transform.DOMove(currentTile.leftTile.transform.position, 0.5f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        currentTile.leftTile.SetPlayerHere(Global.Player);
                        base.ApplyCardEffect();
                    });
            }
            else
            {
                DOVirtual.DelayedCall(0.5f, () => { base.ApplyCardEffect(); });
            }
        }
    }
}