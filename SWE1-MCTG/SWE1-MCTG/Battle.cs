using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    class Battle
    {
        List<User> players = new List<User>();

        string BattleName { get; set; }

        public Battle(string name)
        {
            this.BattleName = name;
        }

        public void AddUser(User u)
        {
            this.players.Add(u);
        }

        public string GetName()
        {
            return BattleName;
        }

        public List<User> ListPlayers()
        {
            return players;
        }
    }
}
