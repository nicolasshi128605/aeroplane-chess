using UnityEngine;

namespace CardEffect
{
    public class MoveDownOne : Card.CardEffect
    {
        public override void ApplyCardEffect()
        {
            Debug.Log("MoveDownOne");
            base.ApplyCardEffect();
        }
    }
}