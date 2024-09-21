using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardDescription : MonoBehaviour
    {
        public TMP_Text text;

        private void Awake()
        {
            Global.CardDescription = this;
        }
    }
}