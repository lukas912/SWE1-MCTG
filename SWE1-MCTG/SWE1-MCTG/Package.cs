using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SWE1_MCTG
{
    class Package
    {
        List<ICard> cards = new List<ICard>();
        string packageName { get; set; }
        int price { get; set; }

        public Package(string name, int price)
        {
            this.packageName = name;
            this.price = price;
        }

        public string getName()
        {
            return packageName;
        }

        public int getPrice()
        {
            return this.price;
        }

        public void addCards(List<ICard> cards)
        {
            this.cards = cards;
        }
    }
}
