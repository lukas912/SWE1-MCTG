using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SWE1_MCTG
{
    public class Package
    {
        List<ICard> cards = new List<ICard>();
        string PackageName { get; set; }
        int Price { get; set; }

        public Package(string name, int price)
        {
            this.PackageName = name;
            this.Price = price;
        }

        public string GetName()
        {
            return PackageName;
        }

        public int GetPrice()
        {
            return this.Price;
        }

        public void AddCards(List<ICard> cards)
        {
            this.cards = cards;
        }
    }
}
