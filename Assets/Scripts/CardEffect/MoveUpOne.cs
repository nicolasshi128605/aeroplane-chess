using DG.Tweening;
using Managers;

namespace CardEffect
{
    public class MoveUpOne : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            Global.Player.Jump(0.5f);
            if (currentTile.upTile != null)
            {
                Global.Player.ChangeToUp();
                Global.Player.transform.DOMove(currentTile.upTile.transform.position, 0.5f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        currentTile.upTile.SetPlayerHere(Global.Player);
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