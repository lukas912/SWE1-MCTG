using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using NUnit.Framework;

namespace Monster_Trading_Card_Game.MTCG_Tests
{
    [TestFixture]
    class StackTest
    {
        [Test]
        public void addCard()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            st.setUser(myuser);
            MonsterCard card1 = new MonsterCard("karpador", 0);
            MonsterCard card2 = new MonsterCard("pikachu", 10);
            try
            {
                st.addCard(card1);
                st.addCard(card2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void getUser()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            st.setUser(myuser);

            Assert.IsNotNull(st.getUser());
        }

        [Test]
        public void listCards()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            st.setUser(myuser);
            

            Assert.IsNotNull(st.listCards());
        }

        [Test]
        public void removeCard()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            st.setUser(myuser);
            
            MonsterCard card1 = new MonsterCard("karpador", 0);
            MonsterCard card2 = new MonsterCard("pikachu", 10);

            try
            {
                st.addCard(card1);
                st.addCard(card2);
                st.removeCard(card2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
