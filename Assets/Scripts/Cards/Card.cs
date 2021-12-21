using System;
using System.Collections;
using DG.Tweening;
using Hand;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Cards
{
    public sealed class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        [Header("Animation Parameters")] 
        [SerializeField] private float moveDuration = 0.25f;
        [SerializeField] private float destroyAnimationDuration = 0.5f;

        [Header("References")]
        [SerializeField] private Ease moveEase;
        [SerializeField] private CardUI cardUI;


        private Vector2 _initialPosition;
        private bool _isHovering;
        private Camera _camera;
        private CardStats _cardStats;

        public bool CanInteract { get; private set; }
        public bool CanDrag { get; private set; }
        public CardInfo CardInfo { get; private set; }
        public Action OnCardComeBackToHand;
        public Action<Card> OnCardDestroy;

        private void Awake() => _camera = Camera.main;

        public void InitializeCard(CardInfo cardInfo)
        {
            CardInfo = cardInfo;
            cardUI.PopulateCardUI(cardInfo);
            _cardStats = new CardStats(cardInfo);
            OnCardDestroy += Die;
        }

        public void SetInitialPosition() => _initialPosition = transform.localPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            ForbidHovers();
            cardUI.ScaleDown(_initialPosition.y);
            cardUI.SetMaterial(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            MoveToPosition(eventData.position);
        }

        private void MoveToPosition(Vector3 position)
        {
            position.z = 10.0f;
            transform.position = _camera.ScreenToWorldPoint(position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            cardUI.SetMaterial(false);
            if (!CanDrag) return;
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DragZone dragZone);
                if (dragZone != null) transform.SetParent(dragZone.transform);
                else ReturnCardBack();
            }
            else
            {
                ReturnCardBack();
            }
        }

        private void ReturnCardBack()
        {
            transform.DOLocalMove(_initialPosition, moveDuration).SetEase(moveEase)
                .OnComplete(() =>
                {
                    OnCardComeBackToHand?.Invoke();
                    AllowInterActions();
                });
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanInteract) return;
            if (_isHovering) return;
            cardUI.ScaleUp(_initialPosition.y);
            _isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHovering) return;
            cardUI.ScaleDown(_initialPosition.y);
            _isHovering = false;
        }


        public void ChangeValueRandomly(int value, float delay)
        {
            var randomStat = (StatsEnum)Random.Range(0, 3);

            StartCoroutine(WaitForDelay());

            IEnumerator WaitForDelay()
            {
                yield return new WaitForSeconds(delay);
                _cardStats.RandomlyChangeValue(randomStat, value);
                cardUI.ChangeCardUI(_cardStats);
                CheckHealth();
            }
        }
        private void CheckHealth()
        {
            if (_cardStats.Health <= 0) OnCardDestroy?.Invoke(this);
        }

        public void ForbidInterActions()
        {
            ForbidHovers();
            ForbidDrags();
        }

        public void AllowInterActions()
        {
            AllowHovers();
            AllowDrags();
        }

        private void Die(Card card)
        {
            transform.DOScale(0, destroyAnimationDuration).OnComplete((() =>
            {
                Destroy(gameObject);
            }));
        }

        private void OnDestroy() => OnCardDestroy -= Die;

        public void ForbidDrags() => CanDrag = false;
        public void AllowDrags() => CanDrag = true;
        public void AllowHovers() => CanInteract = true;
        public void ForbidHovers() => CanInteract = false;
    }
}