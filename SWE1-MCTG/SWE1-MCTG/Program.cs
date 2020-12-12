using System;

namespace SWE1_MCTG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DB_Connection dbc = new DB_Connection();
            dbc.getUsers();
        }
    }
}