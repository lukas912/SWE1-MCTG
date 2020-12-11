using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Monster_Trading_Card_Game.MTCG_Tests
{
    [TestFixture]
    class CardTest
    {
        [Test]
        public void getName()
        {
            MonsterCard card1 = new MonsterCard("pikachu", 10);
            Assert.IsNotNull(card1.getName());
        }

        [Test]
        public void getDamage()
        {
            MonsterCard card1 = new MonsterCard("pikachu", 10);
            Assert.IsNotNull(card1.getDamage());
        }

        [Test]
        public void getCardType()
        {
            MonsterCard card1 = new MonsterCard("pikachu", 10);
            Assert.IsNotNull(card1.getCardType());
        }
    }
}
