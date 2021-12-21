using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Hand
{
    public sealed class PlayerHand : MonoBehaviour
    {
        //Made for ease, in a real project will be determined online
        [SerializeField] private bool isMe;
        [SerializeField] private CurvedLayout curvedLayout;

        public bool IsMe => isMe;
        public List<Card> CardsInHand { get; private set; }

        private void Awake()
        {
            CardsInHand = new List<Card>();
        }

        public void AddCardToHand(Card card)
        {
            CardsInHand.Add(card);
            card.OnCardComeBackToHand += OnCardComeBackToHand;
        }


        public void RemoveCardFromHand(Card card)
        {
            CardsInHand.Remove(card);
            card.OnCardComeBackToHand -= OnCardComeBackToHand;
        }

        private void OnCardComeBackToHand() => curvedLayout.CalculateRadial();
    }
}