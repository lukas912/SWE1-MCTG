using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    interface ICard
    {
        string name { get; set; }
        int damage { get; set; }
        string cardType { get; set; }

        string getName();
        int getDamage();
        string getCardType();

    }
}
