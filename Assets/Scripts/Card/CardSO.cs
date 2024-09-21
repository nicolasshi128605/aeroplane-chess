using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CreateCard", order = 1)]
    public class CardSO : ScriptableObject
    {
        public string cardName;
        public string displayName;
        public Sprite image;
        public string description;
    }
}