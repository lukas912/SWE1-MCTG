using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class SpellCard
    {
        public string name { get; set; }
        public int damage { get; set; }
        public string cardType { get; set; }

        public SpellCard(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
            this.cardType = "Spell Card";
        }

        public string getName()
        {
            return name;
        }

        public int getDamage()
        {
            return damage;
        }

        public string getCardType()
        {
            return cardType;
        }
    }
}
