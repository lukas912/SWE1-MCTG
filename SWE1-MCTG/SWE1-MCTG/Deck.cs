using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Deck
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

        public List<ICard> listCardsIncluded()
        {
            return cards;
        }
    }
}
