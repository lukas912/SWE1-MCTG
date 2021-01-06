using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;


namespace MTCG_Tests
{
    [TestFixture]
    class CardTest
    {
        [Test]
        public void getName()
        {
            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            Assert.IsNotNull(card1.GetName());
        }

        [Test]
        public void getDamage()
        {
            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            Assert.IsNotNull(card1.GetDamage());
        }

        [Test]
        public void getCardType()
        {
            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            Assert.IsNotNull(card1.GetCardType());
        }
    }
}
