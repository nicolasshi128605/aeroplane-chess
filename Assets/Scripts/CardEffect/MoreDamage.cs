using Managers;

namespace CardEffect
{
    public class MoreDamage : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            Global.doubleDMG = true;
            base.ApplyCardEffect();
        }
    }
}