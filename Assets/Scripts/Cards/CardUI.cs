using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public sealed class CardUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Text title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI manaText;

        [Header("Scale Animation Parameters")] 
        [SerializeField] private float scaleValue = 1.8f;
        [SerializeField] private float scaleDuration = 0.1f;
        [SerializeField] private Ease scaleEase = Ease.InOutQuad;
        [SerializeField] private float scaleMove = 100f;

        [Header("Object References")]
        [SerializeField] private GameObject cardBack;
        [SerializeField] private Card card;
        [SerializeField] private Material material;
        [SerializeField] private Image cardImage;
        public void PopulateCardUI(CardInfo cardInfo)
        {
            title.text = cardInfo.CardName;
            description.text = cardInfo.CardDescription;
            attackText.text = cardInfo.Attack.ToString();
            healthText.text = cardInfo.Health.ToString();
            manaText.text = cardInfo.ManaCost.ToString();
        }

        public void ChangeCardUI(CardStats cardStats)
        {
            attackText.text = cardStats.Attack.ToString();
            healthText.text = cardStats.Health.ToString();
            manaText.text = cardStats.ManaCost.ToString();
        }

        public void ToggleCardBack() => cardBack.SetActive(!cardBack.activeInHierarchy);
        public void ScaleDown(float initialPositionInHand)
        {
            transform.DOLocalMoveY(initialPositionInHand, scaleDuration).SetEase(scaleEase);
            transform.DOScale(1, scaleDuration);
        }

        public void SetMaterial(bool value) => cardImage.material = value ? material : null;

        public void ScaleUp(float initialPositionInHand)
        {
            transform.DOLocalMoveY(initialPositionInHand + scaleMove, scaleDuration).SetEase(scaleEase);
            transform.DOScale(scaleValue, scaleDuration);
        }
    }
}