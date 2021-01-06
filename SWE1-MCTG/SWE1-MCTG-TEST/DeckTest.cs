using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;

namespace MTCG_Tests
{
    [TestFixture]
    class DeckTest
    {
        [Test]
        public void getUserTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");

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

            Deck d = new Deck(mycards, myuser);

            Assert.IsNotNull(d.GetUser());
        }

        [Test]
        public void listCardsIncluded()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");

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

            Deck d = new Deck(mycards, myuser);

            try
            {
                d.ListCardsIncluded();
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
