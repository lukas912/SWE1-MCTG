using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public interface ICard
    {
        string CardID { get; set; }
        string Name { get; set; }
        double Damage { get; set; }
        string CardType { get; set; }
        string PackageID { get; set; }
        double Weakness { get; set; }

        string GetName();
        double GetDamage();
        string GetCardType();

    }
}
