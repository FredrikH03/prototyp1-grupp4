using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace real_time_horror_group4
{
    public class Authenticator
    {
        private HttpListenerContext _httpListenerContext;
        
        public Authenticator(HttpListenerContext httpListenerContext)
        {
            List<User> users = new List<User>();
          _httpListenerContext = httpListenerContext;
            switch (_httpListenerContext.Request.Url.AbsolutePath)
            {
                case "/login/":
                {
                        Login(users);
                        break;
                }
                case "/register/":
                {
                        Register(users);
                        break;
                }
            }
        }

        private void OutputMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _httpListenerContext.Response.OutputStream.Write(buffer);
        }

        private bool Login(List<User> users)
        {
            bool success = false;
            OutputMessage("<Username>,<Password>");
            string[] userLoginRequest = _httpListenerContext.Request.InputStream.ToString().Split(',');

            foreach (User user in users)
            {
                if (userLoginRequest[0] == user.Username && userLoginRequest[1] == user.Password)
                {
                    OutputMessage($@"Successful authentication. 
                                                      Your session ID is {user.Guid}.
                                                      Usage: <ID>,<commands>");
                    //Save session GUID to database.
                    success = true;
                }
            }
            return success;
        }

        public void Register(List<User> users)
        {
            OutputMessage("<Username>,<Password>");
            string[] userRegistrationRequest = _httpListenerContext.
                                                                      Request.
                                                                      InputStream.
                                                                      ToString().
                                                                      Split(',');
            users.Add(new User(userRegistrationRequest[0], userRegistrationRequest[1]));

            string successfullRegistrationMessage = "Successful user registration";
            OutputMessage(successfullRegistrationMessage);
            Console.WriteLine(successfullRegistrationMessage);
        }
    }
}
