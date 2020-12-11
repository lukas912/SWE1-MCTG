using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Battle
    {
        List<User> players = new List<User>();

        string battleName { get; set; }

        public Battle(string name)
        {
            this.battleName = name;
        }

        public void addUser(User u)
        {
            this.players.Add(u);
        }

        public string getName()
        {
            return battleName;
        }

        public List<User> listPlayers()
        {
            return players;
        }
    }
}
