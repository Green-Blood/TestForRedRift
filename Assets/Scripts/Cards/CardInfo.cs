using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public sealed class CardInfo : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField] private string cardDescription;
        [SerializeField] private int manaCost;
        [SerializeField] private int attack;
        [SerializeField] private int health;

        public string CardName => cardName;
        public string CardDescription => cardDescription;
        public int ManaCost => manaCost;
        public int Attack => attack;
        public int Health => health;
    }
}