using System;
using System.Collections.Generic;
using System.Text;

namespace SWE1_MCTG
{
    class Session
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Session_id { get; set; }

        public Session(string u, string p, string s)
        {
            this.Username = u;
            this.Password = p;
            this.Session_id = s;
        }


    }
}
