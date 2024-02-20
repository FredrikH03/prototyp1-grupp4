using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real_time_horror_group4
{
    public class User
    {
        int _id;
        string _username;
        string _password;

        public int Id => _id;

        public string Username => _username; 
        public string Password => _password; 

        public User(string username, string password)
        { 
            _username = username;
            _password = password;
        }
    }
}
