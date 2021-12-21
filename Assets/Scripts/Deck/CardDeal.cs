using System;
using System.Collections;
using Cards;
using DG.Tweening;
using Hand;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deck
{
    public sealed class CardDeal : MonoBehaviour
    {
        [SerializeField] private PlayerSetup playerSetup;
        [SerializeField] private DeckSetup deckSetup;
        [SerializeField] private float dealAnimationDuration = 0.75f;
        [SerializeField] private float dealDelay = 0.15f;
        [SerializeField] private Ease dealAnimationEase = Ease.OutBounce;


        private const int MinDealAmount = 4;
        private const int MaxDealAmount = 6;
        private int _dealAmount;

        private void Awake() => _dealAmount = RandomizeDealAmount();
        private int RandomizeDealAmount() => Random.Range(MinDealAmount, MaxDealAmount + 1);
        private void Start() => DealCards();

        public void DealCards()
        {
            foreach (var playerHand in playerSetup.PlayerHands)
            {
                for (int index = 0; index < _dealAmount; index++)
                {
                    var card = deckSetup.CardsInDeck[index];

                    deckSetup.TakeFromDeck(card);
                    playerHand.AddCardToHand(card);

                    float delayDuration = index * dealDelay * dealAnimationDuration;
                    var hand = playerHand;
                    var endValue = new Vector2(hand.transform.position.x - (_dealAmount - index), hand.transform.position.y);

                    card.transform.DOMove(
                            endValue
                            , dealAnimationDuration)
                        .SetDelay(delayDuration)
                        .SetEase(dealAnimationEase)
                        .OnComplete((() => { OnMoveComplete(card, hand); }));
                }
            }
        }

        private void OnMoveComplete(Card card, PlayerHand playerHand)
        {
            card.transform.SetParent(playerHand.transform);


            StartCoroutine(WaitForEndOfFrame());

            IEnumerator WaitForEndOfFrame()
            {
                yield return new WaitForEndOfFrame();
                card.SetInitialPosition();
            }


            if (!playerHand.IsMe) return;
            var cardUI = card.GetComponent<CardUI>();
            cardUI.ToggleCardBack();
            card.AllowInterActions();
        }
    }
}