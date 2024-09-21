using DG.Tweening;
using Managers;

namespace CardEffect
{
    public class MoveRightOne : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            Global.Player.Jump(0.5f);
            if (currentTile.rightTile != null)
            {
                Global.Player.ChangeToRight();
                Global.Player.transform.DOMove(currentTile.rightTile.transform.position, 0.5f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        currentTile.rightTile.SetPlayerHere(Global.Player);
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