using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Monster_Trading_Card_Game.MTCG_Tests
{
    [TestFixture]
    class DeckTest
    {
        [Test]
        public void getUserTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);

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

            Deck d = new Deck(mycards, myuser);

            Assert.IsNotNull(d.getUser());
        }

        [Test]
        public void listCardsIncluded()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);

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

            Deck d = new Deck(mycards, myuser);

            try
            {
                d.listCardsIncluded();
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
