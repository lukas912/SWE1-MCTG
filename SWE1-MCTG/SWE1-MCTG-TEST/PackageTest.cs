using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Monster_Trading_Card_Game.MTCG_Tests
{
    [TestFixture]
    class PackageTest
    {
        [Test]
        public void getNameTest()
        {
            Package p = new Package("Package1", 5);
            Assert.IsNotNull(p.getName());
        }

        [Test]
        public void getPriceTest()
        {
            Package p = new Package("Package1", 5);
            Assert.IsNotNull(p.getPrice());
        }

        [Test]
        public void addCardsTest()
        {
            Package p = new Package("Package1", 5);
            MonsterCard c1 = new MonsterCard("garados", 120);
            MonsterCard c2 = new MonsterCard("pichu", 12);
            MonsterCard c3 = new MonsterCard("pikachu", 100);
            MonsterCard c4 = new MonsterCard("karpador", 0);
            MonsterCard c5 = new MonsterCard("lapras", 150);
            List<ICard> mycards = new List<ICard>();
            mycards.Add(c1);
            mycards.Add(c2);
            mycards.Add(c3);
            mycards.Add(c4);
            mycards.Add(c5);
            

            try
            {
                p.addCards(mycards);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
