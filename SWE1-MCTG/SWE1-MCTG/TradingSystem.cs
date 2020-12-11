using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class TradingSystem
    {
        static public void tradeCard(ICard card1, ICard card2, User user1, User user2)
        {
            user1.getStack().addCard(card2);
            user2.getStack().addCard(card1);
            user2.getStack().removeCard(card1);
            user2.getStack().removeCard(card2);
        }
    }
}
