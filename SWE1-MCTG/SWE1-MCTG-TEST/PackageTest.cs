using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;

namespace MTCG_Tests
{
    [TestFixture]
    class PackageTest
    {
        [Test]
        public void getNameTest()
        {
            Package p = new Package("Package1", 5);
            Assert.IsNotNull(p.GetName());
        }

        [Test]
        public void getPriceTest()
        {
            Package p = new Package("Package1", 5);
            Assert.IsNotNull(p.GetPrice());
        }

        [Test]
        public void addCardsTest()
        {
            Package p = new Package("Package1", 5);
            MonsterCard c1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard c2 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard c3 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard c4 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard c5 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            List<ICard> mycards = new List<ICard>();
            mycards.Add(c1);
            mycards.Add(c2);
            mycards.Add(c3);
            mycards.Add(c4);
            mycards.Add(c5);
            

            try
            {
                p.AddCards(mycards);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
