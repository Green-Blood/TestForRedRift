using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Deck
{
    public sealed class DeckSetup : MonoBehaviour
    {
        [SerializeField] private Card cardPrefab;
        [SerializeField] private CardInfo[] cardsInfo = Array.Empty<CardInfo>();
        public List<Card> CardsInDeck { get; private set; }

#if UNITY_EDITOR
        private void OnValidate() => cardsInfo = LoadAllCards();
        [ContextMenu("LoadCards")]
        public CardInfo[] LoadAllCards()
        {
            cardsInfo = Resources.LoadAll<CardInfo>("Scriptable Objects/Cards");
            return cardsInfo;
        }
#endif
        private void Awake()
        {
            CardsInDeck = new List<Card>();
            InstantiateDeck();
        }

        private void InstantiateDeck()
        {
            foreach (var cardInfo in cardsInfo)
            {
                var card = Instantiate(cardPrefab, transform);
                card.InitializeCard(cardInfo);
                CardsInDeck.Add(card);
            }
        }

        public void TakeFromDeck(Card card) => CardsInDeck.Remove(card);
    }
}