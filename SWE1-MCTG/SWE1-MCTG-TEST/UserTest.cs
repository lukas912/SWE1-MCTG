using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SWE1_MCTG;

namespace MTCG_Tests
{
    [TestFixture]
    class UserTest
    {

        [Test]
        public void GetUsernameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            Assert.IsNotNull(myuser.GetUsername());
        }

        [Test]
        public void GetdisplayNameTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            Assert.IsNotNull(myuser.GetdisplayName());
        }

        [Test]
        public void GetEmailTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            Assert.IsNotNull(myuser.GetEmail());
        }

        [Test]
        public void SetEmailTest()
        {
            Stack st = new Stack();
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            try
            {
                myuser.SetEmail("lk@hotmail.com");
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
            User myuser = new User("Lukas2511", "lukas", "lukas.n912@gmail.com", "1234", 20, "img", "bio");
            try
            {
                myuser.SetDisplayName("max");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
