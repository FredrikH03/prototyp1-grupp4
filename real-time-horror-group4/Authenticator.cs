using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace real_time_horror_group4
{
    public class Authenticator
    {
        private NpgsqlDataSource _database;
        private HttpListenerContext _httpListenerContext;

        public  Authenticator(HttpListenerContext httpListenerContext, NpgsqlDataSource database)
        {
            _database = database;
            _httpListenerContext = httpListenerContext;

            switch (_httpListenerContext.Request.Url.AbsolutePath)
            {
                case "/login":
                {
                        Login(GetUsers().Result);
                        break;
                }
                case "/register":
                {
                        Register(GetUsers().Result);
                        break;
                }
            }
        }

        private async Task<List<User>> GetUsers() 
        {
            List<User> users = new List<User>();
            await using NpgsqlCommand command = _database.CreateCommand("SELECT username, password FROM users");
            await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(new User(reader.GetString(0), reader.GetString(1)));
            }
            return users;
        }

        private void OutputMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _httpListenerContext.Response.OutputStream.Write(buffer);
        }

        private bool Login(List<User> users)
        {
            bool success = false;
            string[] userLoginRequest = _httpListenerContext.Request.InputStream.ToString().Split(',');

            foreach (User user in users)
            {
                if (userLoginRequest[0] == user.Username && userLoginRequest[1] == user.Password)
                {
                    OutputMessage($@"Successful authentication. 
                                                      Your session ID is {user.Id}.
                                                      Usage: <ID>,<commands>");
                    
                    string successfullLoginMessage = "Successful login registration";
                    OutputMessage(successfullLoginMessage);
                    Console.WriteLine(successfullLoginMessage);

                    success = true;
                }
            }
            return success;
        }

        private async Task AddUserToDatabase(User user)
        {
            string command = @$"INSERT INTO users(username, password)
                                                 VALUES({user.Username}, {user.Password})";
            await _database.CreateCommand(command).ExecuteNonQueryAsync();
        }

        public async Task Register(List<User> users)
        {            
            string[] userRegistrationRequest = _httpListenerContext.
                                                                      Request.
                                                                      InputStream.
                                                                      ToString().
                                                                      Split(',');
            User registratedUser = new User(userRegistrationRequest[0], userRegistrationRequest[1]);
            users.Add(registratedUser);
            await AddUserToDatabase(registratedUser);

            string successfullRegistrationMessage = "Successful user registration";
            OutputMessage(successfullRegistrationMessage);
            Console.WriteLine(successfullRegistrationMessage);
        }
    }
}
