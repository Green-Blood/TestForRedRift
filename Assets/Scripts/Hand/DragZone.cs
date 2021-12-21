using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hand
{
    public sealed class DragZone : MonoBehaviour, IDropHandler
    {
        public List<Card> CardsInZone { get; private set; }
        [SerializeField] private PlayerHand dragZonePlayerHand;

        private void Awake()
        {
            CardsInZone = new List<Card>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.TryGetComponent(out Card card);
            if (card == null) return;
            if (!card.CanDrag) return;
            card.ForbidInterActions();
            CardsInZone.Add(card);
            card.transform.SetParent(transform);
            card.OnCardDestroy += OnCardDestroy;
            dragZonePlayerHand.RemoveCardFromHand(card);
        }

        private void OnCardDestroy(Card card)
        {
            card.OnCardDestroy -= OnCardDestroy;
            CardsInZone.Remove(card);
        }
    }
}