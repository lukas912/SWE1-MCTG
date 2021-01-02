using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace SWE1_MCTG
{
    public class DB_Connection
    {
        public void test(string username, string password)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "INSERT INTO \"Users\" (username, password) VALUES ()";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt16(3), rdr.GetString(4));
            }
        }

        public bool createUser(User user)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Users\" (username, password, coins, display_name, email, image, bio) VALUES (@u, @p, @c, @dname, @mail, @img, @bio)", con))
                {
                    cmd.Parameters.AddWithValue("u", user.username);
                    cmd.Parameters.AddWithValue("p", user.password);
                    cmd.Parameters.AddWithValue("c", user.coins);
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

        public bool loginUser(string username, string password)
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

        public string createPackage()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string id = createRandomID(20);

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

        public List<ICard> showUserDeck(string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Deck_Cards\" JOIN \"Card\" USING(card_id) WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", getUsernameByToken(username));


                using (var reader = cmd.ExecuteReader())
                {
                    var i = 0;
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

        public string getUserStats(string token)
        {
            if(getUsernameByToken(token) != null)
            {
                return "some stats";
            } 

            else
            {
                return "No user found";
            }
        }

        public string getUserScore(string token)
        {
            if (getUsernameByToken(token) != null)
            {
                return "user score";
            }

            else
            {
                return "No user found";
            }
        }



        public User showUserData(string token, string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            User user = null;

            if(getUsernameByToken(token) == username)
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE username = @uid", con))
                {
                    cmd.Parameters.AddWithValue("uid", username);



                    using (var reader = cmd.ExecuteReader())
                    {
                        var i = 0;
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

        public bool addCardsToPackage(string packageID, List<ICard> cards)
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
                        cmd.Parameters.AddWithValue("cid", card.cardID);
                        cmd.Parameters.AddWithValue("name", card.name);
                        cmd.Parameters.AddWithValue("damage", card.damage);
                        cmd.Parameters.AddWithValue("pid", card.packageID);
                        cmd.Parameters.AddWithValue("ctype", card.cardType);
                        cmd.Parameters.AddWithValue("weak", card.weakness);
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

        public bool checkUser(string username, string password)
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
                    return true;
                }

                else
                {
                    return false;
                }


            }
        }

        public string getUsernameByToken(string token)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
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

            output = usernames.Find(obj => { return token.Contains(obj); });
            return output;
        }

        public bool editUserData(User newuser)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();


                try
                {
                using (var cmd = new NpgsqlCommand("UPDATE \"Users\" SET display_name = @dname, bio = @bio, image = @img WHERE username = @uid", con))
                    {
                        cmd.Parameters.AddWithValue("uid", newuser.username);
                        cmd.Parameters.AddWithValue("dname", newuser.displayName);
                        cmd.Parameters.AddWithValue("bio", newuser.bio);
                        cmd.Parameters.AddWithValue("img", newuser.image);
                    cmd.ExecuteNonQuery();
                    }

                return true;

                }

                catch
                {
                    return false;
                }

            
        }

        public string getRandomPackageID()
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<string> packages = new List<string>();
            string output;
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Package\"", con))
            {


                using (var reader = cmd.ExecuteReader())
                {
                    var i = 0;
                    while (reader.Read())
                    {
                        packages.Add(reader.GetString(reader.GetOrdinal("package_id")));

                    }
                }


            }

            Random rnd = new Random();
            int r = rnd.Next(packages.Count);
            output = packages[r];
            return output;
        }

        public User getUserByToken(string token)
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
                    var i = 0;
                    while (reader.Read())
                    {
                        User user = new User(reader.GetString(reader.GetOrdinal("username")), "not implemented", "not implemented",
                            reader.GetString(reader.GetOrdinal("password")), reader.GetInt16(reader.GetOrdinal("coins")),
                            reader.GetString(reader.GetOrdinal("image")), reader.GetString(reader.GetOrdinal("bio")));
                        users.Add(user);

                    }
                }


            }


            return users.Find(obj => { return getUsernameByToken(token).Contains(obj.username); });

        }


        public bool acquirePackage(string username)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"User_Packages\" (username, package_id) VALUES (@uid, @pid)", con))
                {
                    cmd.Parameters.AddWithValue("uid", getUsernameByToken(username));
                    cmd.Parameters.AddWithValue("pid", getRandomPackageID());
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand("UPDATE \"Users\" SET coins = coins - 5 WHERE username = @uid", con))
                {
                    cmd.Parameters.AddWithValue("uid", getUsernameByToken(username));
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        public List<ICard> showUserCards(string username)
        {
            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Packages\" JOIN \"Card\" USING(package_id) WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", getUsernameByToken(username));


                using (var reader = cmd.ExecuteReader())
                {
                    var i = 0;
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

        public List<ICard> showUserCardsDeck(string username, List<string> cids)
        {

            // Connect to a PostgreSQL database
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            List<ICard> cards = new List<ICard>();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"User_Packages\" JOIN \"Card\" USING(package_id) WHERE username = @uid", con))
            {
                cmd.Parameters.AddWithValue("uid", getUsernameByToken(username));


                using (var reader = cmd.ExecuteReader())
                {
                    var i = 0;
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

            cards = cards.FindAll(obj => { return obj.cardID == cids.Find(obj2 => { return obj2 == obj.cardID; }); });

            return cards;
        }

        public bool createDeck(Deck deck)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";
            string deck_id = getRandomPackageID();
            using var con = new NpgsqlConnection(cs);
            con.Open();


            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Deck\" (deck_id, username) VALUES (@did, @uid)", con))
                {

                    cmd.Parameters.AddWithValue("did", deck_id);
                    cmd.Parameters.AddWithValue("uid", deck.getUser().username);
                    cmd.ExecuteNonQuery();
                }

                foreach(ICard ca in deck.listCardsIncluded())
                {

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"User_Deck_Cards\" (username, deck_id, card_id) VALUES (@uid, @did, @cid)", con))
                    {

                           cmd.Parameters.AddWithValue("did", deck_id);
                           cmd.Parameters.AddWithValue("uid", deck.getUser().username);
                           cmd.Parameters.AddWithValue("cid", ca.cardID);

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

        private string createRandomID(int length)
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
