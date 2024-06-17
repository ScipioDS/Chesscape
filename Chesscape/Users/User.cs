using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Users
{
    public class User
    {
        public string username { get; set; }
        private string password { get; set; }
        private int userELO { get; set; } = 800;
        // CONSTRUCTORS
        public User(string username) 
        {
            this.username = username;
        }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        // UTIL
        public override string ToString()
        {
            return username;
        }
    }
}
