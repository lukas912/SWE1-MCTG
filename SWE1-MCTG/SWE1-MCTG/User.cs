using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class User
    {
        public string username { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int coins { get; set; }
        public string bio { get; set; }
        public string image { get; set; }

        Stack stack;

        public User(string username, string displayName, string email, string password, int coins, string image, string bio)
        {
            this.username = username;
            this.displayName = displayName;
            this.email = email;
            this.password = password;
            this.coins = coins;
            this.image = image;
            this.bio = bio;
        }

        public string getUsername()
        {
            return this.username;
        }

        public Stack getStack()
        {
            return stack;
        }

        public string getdisplayName()
        {
            return this.displayName;
        }

        public string getEmail()
        {
            return this.email;
        }

        public void setDisplayName(string dn)
        {
            if (dn.Length <= 20 && dn.Length > 0)
                this.displayName = dn;
            else
                throw new System.ArgumentException("DisplayName too long or null", "dn");
        }

        public void setEmail(string em)
        {
            if (em.Contains("@"))
                this.email = em;
            else
                throw new System.ArgumentException("Email not valid", "em");

        }

        void selectDeckCards(ICard[] cards)
        {

        }

        void aqcuirePackage(Package package)
        {

        }
    }
}
