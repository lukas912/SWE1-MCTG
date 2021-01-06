using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;

namespace MTCG_Tests
{
    [TestFixture]
    class StackTest
    {
        [Test]
        public void addCard()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st.SetUser(myuser);
            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard card2 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            try
            {
                st.AddCard(card1);
                st.AddCard(card2);
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
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st.SetUser(myuser);

            Assert.IsNotNull(st.GetUser());
        }

        [Test]
        public void listCards()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st.SetUser(myuser);
            

            Assert.IsNotNull(st.ListCards());
        }

        [Test]
        public void removeCard()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            st.SetUser(myuser);

            MonsterCard card1 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);
            MonsterCard card2 = new MonsterCard("0", "pikachu", 10, "test", "test", 10);

            try
            {
                st.AddCard(card1);
                st.AddCard(card2);
                st.RemoveCard(card2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
