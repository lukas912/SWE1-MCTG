using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class User
    {
        string username { get; set; }
        string displayName { get; set; }
        string email { get; set; }
        string password { get; set; }
        int coins { get; set; }

        Stack stack;

        public User(string username, string displayName, string email, string password, Stack stack)
        {
            this.username = username;
            this.displayName = displayName;
            this.email = email;
            this.password = password;
            this.stack = stack;
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
