using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class Deck
    {
        List<ICard> cards = new List<ICard>();
        User User { get; set; }

        public Deck(List<ICard> c, User u)
        {
            this.cards = c;
            this.User = u;
        }

        public User GetUser()
        {
            return User;
        }

        public List<ICard> ListCardsIncluded()
        {
            return cards;
        }
    }
}
