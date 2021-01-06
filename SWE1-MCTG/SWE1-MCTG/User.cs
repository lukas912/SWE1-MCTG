using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    public class User
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }

        Stack stack;

        public User(string username, string displayName, string email, string password, int coins, string image, string bio)
        {
            this.Username = username;
            this.DisplayName = displayName;
            this.Email = email;
            this.Password = password;
            this.Coins = coins;
            this.Image = image;
            this.Bio = bio;
        }

        public string GetUsername()
        {
            return this.Username;
        }

        public Stack GetStack()
        {
            return stack;
        }

        public string GetdisplayName()
        {
            return this.DisplayName;
        }

        public string GetEmail()
        {
            return this.Email;
        }

        public void SetDisplayName(string dn)
        {
            if (dn.Length <= 20 && dn.Length > 0)
                this.DisplayName = dn;
            else
                throw new System.ArgumentException("DisplayName too long or null", "dn");
        }

        public void SetEmail(string em)
        {
            if (em.Contains("@"))
                this.Email = em;
            else
                throw new System.ArgumentException("Email not valid", "em");

        }

        void SelectDeckCards(ICard[] cards)
        {

        }

        void AqcuirePackage(Package package)
        {

        }
    }
}
