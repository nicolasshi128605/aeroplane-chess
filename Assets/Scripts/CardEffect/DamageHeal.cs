using DG.Tweening;
using Managers;

namespace CardEffect
{
    public class DamageHeal : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            Global.DMGHeal = true;
            base.ApplyCardEffect();
        }
    }
}