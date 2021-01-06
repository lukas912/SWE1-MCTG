using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class SpellCard : ICard
    {
        public string CardID { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public double Weakness { get; set; }
        public string CardType { get; set; }
        public string PackageID { get; set; }

        public SpellCard(string cardID, string name, double damage, string packageID, string cardType, double weakness)
        {
            this.CardID = cardID;
            this.Name = name;
            this.Damage = damage;
            this.CardType = "Monste Card";
            this.PackageID = packageID;
            this.CardType = cardType;
            this.Weakness = weakness;
        }

        public string GetName()
        {
            return Name;
        }

        public double GetDamage()
        {
            return Damage;
        }

        public string GetCardType()
        {
            return CardType;
        }
    }
}
