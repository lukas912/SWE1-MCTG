using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Stack
    {
        List<ICard> cards = new List<ICard>();
        User user { get; set; }

        public void setUser(User user)
        {
            this.user = user;
        }

        public void addCard(ICard card)
        {
            try
            {
                cards.Add(card);
            }

            catch
            {
                throw new System.ArgumentException("Card not valid", "card");
            }
            
        }

        public void removeCard(ICard card)
        {
            cards.Remove(card);
        }

        public List<ICard> listCards()
        {
            return cards;
        }

        public User getUser()
        {
            return this.user;
        }
    }
}
