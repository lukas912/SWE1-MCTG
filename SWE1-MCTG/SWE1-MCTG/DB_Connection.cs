using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace SWE1_MCTG
{
    class DB_Connection
    {
        public void getUsers()
        {
             connect();
        }

        private void connect()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=MCTG-DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM \"User\"";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt16(4));
            }
        }

    }
}
