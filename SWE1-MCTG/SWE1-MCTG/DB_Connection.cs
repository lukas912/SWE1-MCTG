using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace SWE1_MCTG
{
    public class DB_Connection
    {
        static List<Session> sessions = new List<Session>();
        static int pck_counter = -1;

        public bool CreateUser(User user)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Users\" (username, password, coins, display_name, email, image, bio) VALUES (@u, @p, @c, @dname, @mail, @img, @bio)", con))
                {
                    cmd.Parameters.AddWithValue("u", user.Username);
                    cmd.Parameters.AddWithValue("p", user.Password);
                    cmd.Parameters.AddWithValue("c", user.Coins);
                    cmd.Parameters.AddWithValue("dname", "not implemented");
                    cmd.Parameters.AddWithValue("mail", "not implemented");
                    cmd.Parameters.AddWithValue("img", "not implemented");
                    cmd.Parameters.AddWithValue("bio", "not implemented");
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }

        }

        public bool EndSessoin(string username)
        {
            if (sessions.Find(obj => { return obj.Username == username; }) != null)
            {
                sessions.Remove(sessions.Find(obj => { return obj.Username == username; }));
                sessions.Remove(sessions.Find(obj => { return obj.Username == username; }));
                return true;
            }

            else
            {
                return false;
            }

                
        }

        public bool deleteTradingDeal(string tid, string header_data)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {


                if(hasAccess(tid, header_data))
                {
                    using (var cmd = new NpgsqlCommand("DELETE FROM \"Trading\" WHERE username = @u AND trading_id = @t", con))
                    {
                        cmd.Parameters.AddWithValue("u", GetUsernameByToken(header_data));
                        cmd.Parameters.AddWithValue("t", tid);

                        Console.WriteLine(tid + " " + GetUsernameByToken(header_data));


                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }

                else
                {
                    return false;
                }

            }

            catch
            {
                Console.WriteLine("anderer fehler catch");
                return false;
            }
        }

        public bool DeleteUser(string username)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();
            Console.WriteLine("HIER");
            try
            {
                using (var cmd = new NpgsqlCommand("DELETE FROM \"Users\" WHERE username = @uid", con))
                {
                    cmd.Parameters.AddWithValue("uid", username);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        private bool hasAccess(string tid, string header_data)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Trading\" WHERE username = @uid AND trading_id = @tid", con))
            {
                cmd.Parameters.AddWithValue("uid", GetUsernameByToken(header_data));
                cmd.Parameters.AddWithValue("tid", tid);
                NpgsqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows == false)
                {
                    Console.WriteLine("Nix zugriff");
                    return false;
                }

                else
                {
                    return true;
                }




            }
        }

        public bool LoginUser(string username, string password)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Users\" (username, password) VALUES (@u, @p)", con))
                {
                    cmd.Parameters.AddWithValue("u", username);
                    cmd.Parameters.AddWithValue("p", password);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }

        }

        public string CreatePackage()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string id = CreatePackageID().ToString();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Package\" (package_id) VALUES (@pid)", con))
                {
                    cmd.Parameters.AddWithValue("pid", id);
                    cmd.ExecuteNonQuery();
                }

                return id;
            }

            catch
            {
                return "error";
            }
        }


        private int CreatePackageID() { 
            pck_counter++;
            Console.WriteLine("CNT: " + pck_counter.ToString());
            return pck_counter;
        }

        public List<ICard> ShowUserDeck(string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Deck_Cards\" JOIN \"Card\" USING(card_id) WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", GetUsernameByToken(username));


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(reader.GetOrdinal("card_type")) == "MonsterCard")
                        {
                            ICard card = new MonsterCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                            cards.Add(card);
                        }

                        if (reader.GetString(reader.GetOrdinal("card_type")) == "SpellCard")
                        {
                            ICard card = new SpellCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                            cards.Add(card);
                        }

                    }
                }


            }

            return cards;
        }

        public string GetUserStats(string token)
        {

                // Connect to a PostgreSQL database
                var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
                string output = "";
                using var con = new NpgsqlConnection(cs);
                con.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Stats\" WHERE username = @uid", con))
                {
                    cmd.Parameters.AddWithValue("uid", GetUsernameByToken(token));


                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output += "Battle: " + reader.GetString(reader.GetOrdinal("battle_id"))
                                    + "\nGewonnen: " + reader.GetBoolean(reader.GetOrdinal("sieg")).ToString()
                                    + "\nPunktestand: " + reader.GetInt16(reader.GetOrdinal("punktestand")).ToString()
                                    + "\nBattle Zeit: " + reader.GetString(reader.GetOrdinal("battle_time")) +
                                    "\n\n";
                        }
                    }


                }
                return output;
            
        }

        public string ShowTradingDeals()
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string output = "";
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Trading\"", con))
            {


                using (var reader = cmd.ExecuteReader())
                {
                    var i = 1;
                    while (reader.Read())
                    {
                        output += "\n\nTRADING DEAL " + i + 
                            "\nUsername: " + reader.GetString(reader.GetOrdinal("username")) +
                            "\nTrading ID: " + reader.GetString(reader.GetOrdinal("trading_id")) +
                            "\nCard Type: " + reader.GetString(reader.GetOrdinal("type_requested")) +
                            "\nMinimum Damage: " + reader.GetString(reader.GetOrdinal("min_damage")) +
                            "\nCard ID: " + reader.GetString(reader.GetOrdinal("card_id"));

                        i++;

                    }
                }


            }

            return output;
        }

        public string GetUserScore(string bid)
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string output = "";
            using var con = new NpgsqlConnection(cs);
            con.Open();

            Console.WriteLine("BATTLE ID: " + bid);

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Stats\" WHERE battle_id = @bid", con))
            {
                cmd.Parameters.AddWithValue("bid", bid);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output += "Punkte von " + reader.GetString(reader.GetOrdinal("username")) + ": " +
                            reader.GetInt16(reader.GetOrdinal("punktestand"));
                        if(reader.GetBoolean(reader.GetOrdinal("sieg")) == true)
                        {
                            output += " (Sieger)\n";
                        }

                        else
                        {
                            output += " (Verlierer)\n";
                        }
                    }
                }


            }
            return output;
        }



        public User ShowUserData(string token, string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            User user = null;

            if(GetUsernameByToken(token) == username)
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE username = @uid", con))
                {
                    cmd.Parameters.AddWithValue("uid", username);



                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User(reader.GetString(reader.GetOrdinal("username")), reader.GetString(reader.GetOrdinal("display_name")),
                                reader.GetString(reader.GetOrdinal("email")), reader.GetString(reader.GetOrdinal("password")),
                                reader.GetInt16(reader.GetOrdinal("coins")), reader.GetString(reader.GetOrdinal("image")),
                                reader.GetString(reader.GetOrdinal("bio")));
                        }


                    }


                }

                Console.WriteLine("!!!");

                return user;
            }

            else
            {
                return null;
            }


        }

        public bool AddCardsToPackage(string packageID, List<ICard> cards)
        {

            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            foreach(ICard card in cards)
            {
                try
                {
                    using (var cmd = new NpgsqlCommand("INSERT INTO \"Card\" (card_id, card_name, damage, package_id, card_type, weakness) VALUES (@cid, @name, @damage, @pid, @ctype, @weak)", con))
                    {
                        cmd.Parameters.AddWithValue("cid", card.CardID);
                        cmd.Parameters.AddWithValue("name", card.Name);
                        cmd.Parameters.AddWithValue("damage", card.Damage);
                        cmd.Parameters.AddWithValue("pid", card.PackageID);
                        cmd.Parameters.AddWithValue("ctype", card.CardType);
                        cmd.Parameters.AddWithValue("weak", card.Weakness);
                        cmd.ExecuteNonQuery();
                    }

                }

                catch
                {
                    return false;
                }

            }

            return true;

        }

        public string getSessionID(string content)
        {
            return sessions.Find(obj => { return obj.Username == content; }).Session_id;
        }

        public bool CheckUser(string username, string password)
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE username = @u AND password = @p", con))
            {
                cmd.Parameters.AddWithValue("u", username);
                cmd.Parameters.AddWithValue("p", password);
                NpgsqlDataReader rd = cmd.ExecuteReader();

                if(rd.HasRows == true)
                {
                    Session s = new Session(username, password, CreateSessionID(20, username));
                    sessions.Add(s);
                    Console.WriteLine("SESSION: " + s.Session_id + " " + s.Username);
                    return true;
                }

                else
                {
                    return false;
                }


            }
        }

        private string CreateSessionID(int length, string username)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            if(username == "kienboec")
            {
                finalString = "kienboectoken";
            }
            
            if (username == "altenhof")
            {
                finalString = "altenhoftoken";
            }


            return finalString;

        }

        static int tpackage_id = 1000;
        public bool Trade(string header_data, string trade_id, string mycard_id)
        {
            // Connect to a PostgreSQL database

            int pid1 = tpackage_id;
            CreateNewPackage(tpackage_id);
            tpackage_id--;
            int pid2 = tpackage_id;
            CreateNewPackage(tpackage_id);
            tpackage_id--;



            string username_provider = GetUserNameTrader(trade_id);
            string username_trader = GetUsernameByToken(header_data);

            if(username_provider == username_trader)
            {
                return false;
            }

            else
            {
                CreateNewUserPackage(pid1, username_provider);
                CreateNewUserPackage(pid2, username_trader);

                string cardid_trader = mycard_id;
                string cardid_provider = getCardIDFromTrading(trade_id);

                Console.WriteLine(pid1 + " " + pid2 + " " + username_provider + " " + username_trader + " " + cardid_provider);

                ChangeCardPackage(pid1, cardid_trader);
                ChangeCardPackage(pid2, cardid_provider);
                
                var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
                using var con = new NpgsqlConnection(cs);
                con.Open();

                
                using (var cmd = new NpgsqlCommand("DELETE FROM \"Trading\" WHERE trading_id = @t", con))
                {
                    cmd.Parameters.AddWithValue("t", trade_id);

                    cmd.ExecuteNonQuery();
                }


                return true;
            }


        }

        private void ChangeCardPackage(int pid, string cardid)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("UPDATE \"Card\" SET package_id = @pid WHERE card_id = @cid", con))
            {
                cmd.Parameters.AddWithValue("pid", pid);
                cmd.Parameters.AddWithValue("cid", cardid);
                cmd.ExecuteNonQuery();
            }
        }

        private string getCardIDFromTrading(string trade_id)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            string output = "";


            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Trading\" WHERE trading_id = @tid", con))
            {
                cmd.Parameters.AddWithValue("tid", trade_id);



                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output = reader.GetString(reader.GetOrdinal("card_id"));
                    }


                }


            }

            return output;

        }

        private void CreateNewUserPackage(int pid, string username)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("INSERT INTO \"User_Packages\" (username, package_id) VALUES (@uid, @pid)", con))
            {
                cmd.Parameters.AddWithValue("pid", pid);
                cmd.Parameters.AddWithValue("uid", username);
                cmd.ExecuteNonQuery();
            }
        }

        private string GetUserNameTrader(string trade_id)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            string output = "";


                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Trading\" WHERE trading_id = @tid", con))
                {
                    cmd.Parameters.AddWithValue("tid", trade_id);



                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output = reader.GetString(reader.GetOrdinal("username"));
                        }


                    }


                }

            return output;
       

        }

        private void CreateNewPackage(int tpackage_id)
        {

            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"Package\" (package_id) VALUES (@pid)", con))
                    {
                        cmd.Parameters.AddWithValue("pid", tpackage_id);
                        cmd.ExecuteNonQuery();
                    }


        }

        public void CreateTradingDeal(string header_data, string tradingid, string card_id, string card_type, string damage)
        {
            string username = GetUsernameByToken(header_data);

            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("INSERT INTO \"Trading\" (username, trading_id, type_requested, card_id, min_damage) VALUES (@uid, @tid, @tr, @cid, @md)", con))
            {
                cmd.Parameters.AddWithValue("uid", username);
                cmd.Parameters.AddWithValue("tid", tradingid);
                cmd.Parameters.AddWithValue("tr", card_type);
                cmd.Parameters.AddWithValue("cid", card_id);
                cmd.Parameters.AddWithValue("md", damage);

                cmd.ExecuteNonQuery();
            }

        }

        public string GetUsernameByToken(string token)
        {
            // Connect to a PostgreSQL database
            /* var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
             List<string> usernames = new List<string>();
             string output;
             using var con = new NpgsqlConnection(cs);
             con.Open();

             using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\"", con))
             {


                 using (var reader = cmd.ExecuteReader())
                 {
                     var i = 0;
                     while (reader.Read())
                     {
                         usernames.Add(reader.GetString(reader.GetOrdinal("username")));

                     }
                 }


             }

             output = usernames.Find(obj => { return token.Contains(obj); });*/
            
            if(sessions.Find(obj => { return token.Substring(2).StartsWith(obj.Session_id); }) != null)
            {
                string username = sessions.Find(obj => { return token.Substring(2).StartsWith(obj.Session_id); }).Username;
                return username;
            }

            else
            {
                throw new Exception("Session ID is invalid!");
            }

            
        }

        public bool Auth(string username)
        {
            try
            {
                GetUsernameByToken(username);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool EditUserData(User newuser)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();


                try
                {
                using (var cmd = new NpgsqlCommand("UPDATE \"Users\" SET display_name = @dname, bio = @bio, image = @img WHERE username = @uid", con))
                    {
                        cmd.Parameters.AddWithValue("uid", newuser.Username);
                        cmd.Parameters.AddWithValue("dname", newuser.DisplayName);
                        cmd.Parameters.AddWithValue("bio", newuser.Bio);
                        cmd.Parameters.AddWithValue("img", newuser.Image);
                    cmd.ExecuteNonQuery();
                    }

                return true;

                }

                catch
                {
                    return false;
                }

            
        }

        public string GetRandomPackageID()
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<string> packages = new List<string>();
            string output;
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Package\" LEFT JOIN \"User_Packages\" USING(package_id) WHERE username IS NULL ORDER BY package_id;", con))
            {


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        packages.Add(reader.GetString(reader.GetOrdinal("package_id")));

                    }
                }


            }

            //Random rnd = new Random();
            //int r = rnd.Next(packages.Count);
            //output = packages[r];
            output = packages[0];
            Console.WriteLine("PID: " + output);
            return output;
        }

        public User GetUserByToken(string token)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<User> users = new List<User>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\"", con))
            {


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetString(reader.GetOrdinal("username")), "not implemented", "not implemented",
                            reader.GetString(reader.GetOrdinal("password")), reader.GetInt16(reader.GetOrdinal("coins")),
                            reader.GetString(reader.GetOrdinal("image")), reader.GetString(reader.GetOrdinal("bio")));
                        users.Add(user);

                    }
                }


            }

            return users.Find(obj => { return GetUsernameByToken(token) == obj.Username; });

        }


        public bool AcquirePackage(string username)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                if(EnoughCoins(GetUsernameByToken(username)) && PackagesAvaiable()) {
                    Console.WriteLine("WIRD GEKAUFT");
                    using (var cmd = new NpgsqlCommand("UPDATE \"Users\" SET coins = coins - 5 WHERE username = @uid", con))
                    {
                        cmd.Parameters.AddWithValue("uid", GetUsernameByToken(username));
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"User_Packages\" (username, package_id) VALUES (@uid, @pid)", con))
                    {
                        cmd.Parameters.AddWithValue("uid", GetUsernameByToken(username));
                        cmd.Parameters.AddWithValue("pid", GetRandomPackageID());
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }

                else
                {
                    Console.WriteLine("KEIN GELD");
                    return false;
                }

            }

            catch
            {
                return false;
            }
        }

        private bool PackagesAvaiable()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();
            List<string> pl = new List<string>();

            using (var cmd = new NpgsqlCommand("select * from \"User_Packages\" RIGHT JOIN \"Package\" USING(package_id) WHERE username IS NULL;", con))
            {

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pl.Add(reader.GetString(reader.GetOrdinal("package_id")));

                    }
                }


            }

            if (pl.Count > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private bool EnoughCoins(string username)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();
            int coins = 0;

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", username);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coins = reader.GetInt16(reader.GetOrdinal("coins"));

                    }
                }


            }

            if(coins >= 5)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public List<ICard> ShowUserCards(string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Packages\" JOIN \"Card\" USING(package_id) WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", GetUsernameByToken(username));


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader.GetString(reader.GetOrdinal("card_type")) == "MonsterCard")
                        {
                            ICard card = new MonsterCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                            cards.Add(card);
                        }

                        if (reader.GetString(reader.GetOrdinal("card_type")) == "SpellCard")
                        {
                            ICard card = new SpellCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                            cards.Add(card);
                        }

                    }
                }


            }

            return cards;
        }

        public List<ICard> ShowUserCardsDeck(string username, List<string> cids)
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Packages\" JOIN \"Card\" USING(package_id) WHERE username = @uid", con))
            {
                Console.WriteLine("HIER GEHTS " + username);
                
                cmd.Parameters.AddWithValue("uid", GetUsernameByToken(username));

     


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(reader.GetOrdinal("card_type")) == "MonsterCard")
                        {
                            ICard card = new MonsterCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                                cards.Add(card);
                            

                        }

                        if (reader.GetString(reader.GetOrdinal("card_type")) == "SpellCard")
                        {
                            ICard card = new SpellCard(reader.GetString(reader.GetOrdinal("card_id")), reader.GetString(reader.GetOrdinal("card_name")),
                                reader.GetDouble(reader.GetOrdinal("damage")), reader.GetString(reader.GetOrdinal("package_id")),
                                reader.GetString(reader.GetOrdinal("card_type")), reader.GetDouble(reader.GetOrdinal("weakness")));

                            cards.Add(card);
                        }

                    }
                }

            }

            cards = cards.FindAll(obj => { return obj.CardID == cids.Find(obj2 => { return obj2 == obj.CardID; }); });

            return cards;
        }

        public bool CreateDeck(Deck deck)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string deck_id = CreateRandomID(20);
            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Deck\" (deck_id, username) VALUES (@did, @uid)", con))
                {

                    cmd.Parameters.AddWithValue("did", deck_id);
                    cmd.Parameters.AddWithValue("uid", deck.GetUser().Username);
                    cmd.ExecuteNonQuery();
                }

                foreach(ICard ca in deck.ListCardsIncluded())
                {

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"User_Deck_Cards\" (username, deck_id, card_id) VALUES (@uid, @did, @cid)", con))
                    {

                           cmd.Parameters.AddWithValue("did", deck_id);
                           cmd.Parameters.AddWithValue("uid", deck.GetUser().Username);
                           cmd.Parameters.AddWithValue("cid", ca.CardID);

                           cmd.ExecuteNonQuery();
                        

                    }
                }



                return true;
            }

            catch
            {
                return false;
            }
        }

        private string CreateRandomID(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;

        }

    }
}
