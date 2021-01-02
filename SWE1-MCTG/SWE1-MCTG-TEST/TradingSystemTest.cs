using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG_Tests
{
    [TestFixture]
    class TradingSystemTest
    {
        [Test]
        public void tradeCardTest()
        {
            Stack st1 = new Stack();
            User myuser1 = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st1);
            st1.setUser(myuser1);

            Stack st2 = new Stack();
            User myuser2 = new User("Lukas2512", "lukas2", "lukas.n912@gmail.at", "12134", st2);
            st2.setUser(myuser2);

            MonsterCard card1 = new MonsterCard("karpador", 0);
            MonsterCard card2 = new MonsterCard("pikachu", 10);

            st1.addCard(card1);
            st2.addCard(card2);

            
            try
            {
                TradingSystem.tradeCard(card1, card2, myuser1, myuser2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
