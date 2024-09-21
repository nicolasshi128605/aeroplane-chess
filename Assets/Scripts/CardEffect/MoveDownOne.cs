using DG.Tweening;
using Managers;

namespace CardEffect
{
    public class MoveDownOne : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            var currentTile = Global.Player.currentTIle;
            Global.Player.Jump(0.5f);
            if (currentTile.downTile != null)
            {
                Global.Player.ChangeToDown();
                Global.Player.transform.DOMove(currentTile.downTile.transform.position, 0.5f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        currentTile.downTile.SetPlayerHere(Global.Player);
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