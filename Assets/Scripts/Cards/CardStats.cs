using System;

namespace Cards
{
    public sealed class CardStats
    {
        private int _attack;
        private int _manaCost;
        private int _health;
        public int Attack => _attack;
        public int ManaCost => _manaCost;
        public int Health => _health;

        public CardStats(CardInfo cardInfo)
        {
            _attack = cardInfo.Attack;
            _manaCost = cardInfo.ManaCost;
            _health = cardInfo.Health;
        }

        public void RandomlyChangeValue(StatsEnum statsEnumEnum, int changeValue)
        {
            switch (statsEnumEnum)
            {
                case StatsEnum.Attack:
                    _attack -= changeValue;
                    break;
                case StatsEnum.Health:
                    _health -= changeValue;
                    break;
                case StatsEnum.Mana:
                    _manaCost -= changeValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(statsEnumEnum), statsEnumEnum, null);
            }
        }

        
    }
}