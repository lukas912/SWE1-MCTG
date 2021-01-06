using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG_Tests
{
    [TestFixture]
    class UserTest
    {

        [Test]
        public void GetUsernameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            Assert.IsNotNull(myuser.getUsername());
        }

        [Test]
        public void GetdisplayNameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            Assert.IsNotNull(myuser.getdisplayName());
        }

        [Test]
        public void GetEmailTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            Assert.IsNotNull(myuser.getEmail());
        }

        [Test]
        public void SetEmailTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            try
            {
                myuser.setEmail("lk@hotmail.com");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void SetDisplayNameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", st);
            try
            {
                myuser.setDisplayName("max");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
