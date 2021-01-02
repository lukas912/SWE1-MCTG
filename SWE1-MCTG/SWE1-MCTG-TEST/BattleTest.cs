using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG_Tests
{
    [TestFixture]
    class BattleTest
    {
        [Test]
        public void getNameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            User myuser2 = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            List<User> u = new List<User>();
            u.Add(myuser);
            u.Add(myuser2);
            Battle b = new Battle("Battle 1");
            b.addUser(myuser);
            b.addUser(myuser2);
            Assert.IsNotNull(b.getName());
        }

        [Test]
        public void listPlayersTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            User myuser2 = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            List<User> u = new List<User>();
            u.Add(myuser);
            u.Add(myuser2);
            Battle b = new Battle("Battle 1");
            b.addUser(myuser);
            b.addUser(myuser2);

            try
            {
                b.listPlayers();
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void addUserTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            User myuser2 = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            List<User> u = new List<User>();
            u.Add(myuser);
            u.Add(myuser2);
            Battle b = new Battle("Battle 1");


            try
            {
                b.addUser(myuser);
                b.addUser(myuser2);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
