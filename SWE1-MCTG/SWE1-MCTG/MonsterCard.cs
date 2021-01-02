using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class MonsterCard : ICard
    {
        public string cardID { get; set; }
        public string name { get; set; }
        public double damage { get; set; }
        public double weakness { get; set; }
        public string cardType { get; set; }
        public string packageID { get; set; }


        public MonsterCard(string cardID, string name, double damage, string packageID, string cardType, double weakness)
        {
            this.cardID = cardID;
            this.name = name;
            this.damage = damage;
            this.cardType = "Monste Card";
            this.packageID = packageID;
            this.cardType = cardType;
            this.weakness = weakness;
        }

        public string getName()
        {
            return name;
        }

        public double getDamage()
        {
            return damage;
        }

        public string getCardType()
        {
            return cardType;
        }
    }
}
