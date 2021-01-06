using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWE1_MCTG
{
    public class Stack
    {
        List<ICard> cards = new List<ICard>();
        User User { get; set; }

        public void SetUser(User user)
        {
            this.User = user;
        }

        public void AddCard(ICard card)
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

        public void RemoveCard(ICard card)
        {
            cards.Remove(card);
        }

        public List<ICard> ListCards()
        {
            return cards;
        }

        public User GetUser()
        {
            return this.User;
        }
    }
}
