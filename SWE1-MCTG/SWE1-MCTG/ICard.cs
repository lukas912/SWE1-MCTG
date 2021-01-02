using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public interface ICard
    {
        string cardID { get; set; }
        string name { get; set; }
        double damage { get; set; }
        string cardType { get; set; }
        string packageID { get; set; }
        double weakness { get; set; }

        string getName();
        double getDamage();
        string getCardType();

    }
}
