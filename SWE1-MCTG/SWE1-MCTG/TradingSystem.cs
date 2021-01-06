using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class TradingSystem
    {
        static public void TradeCard(ICard card1, ICard card2, User user1, User user2)
        {
            user1.GetStack().AddCard(card2);
            user2.GetStack().AddCard(card1);
            user2.GetStack().RemoveCard(card1);
            user2.GetStack().RemoveCard(card2);
        }
    }
}
