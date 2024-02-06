using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real_time_horror_group4
{
    public class User
    {
        Guid _guid;
        string _username;
        string _password;

        public Guid Guid
        {
            get => _guid;
        }

        public string Username
        {
            get => _username; 
        }

        public string Password 
        {
            get => _password; 
        }

        public User(string username, string password)
        { 
            _username = username;
            _password = password;
            _guid = Guid.NewGuid();
        }
    }
}
