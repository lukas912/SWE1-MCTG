using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;

namespace MTCG_Tests
{
    [TestFixture]
    class TradingSystemTest
    {
        //[Test]
        public void tradeCardTest()
        {
            Stack st1 = new Stack();
            User myuser1 = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st1.SetUser(myuser1);

            Stack st2 = new Stack();
            User myuser2= new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st2.SetUser(myuser2);

            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard card2 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);

            st1.AddCard(card1);
            st2.AddCard(card2);

            
            try
            {
                TradingSystem.TradeCard(card1, card2, myuser1, myuser2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
