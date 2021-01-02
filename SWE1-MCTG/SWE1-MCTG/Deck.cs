using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class Deck
    {
        List<ICard> cards = new List<ICard>();
        User user { get; set; }

        public Deck(List<ICard> c, User u)
        {
            this.cards = c;
            this.user = u;
        }

        public User getUser()
        {
            return user;
        }

        public string test()
        {
            return "test";
        }

        public List<ICard> listCardsIncluded()
        {
            return cards;
        }
    }
}
