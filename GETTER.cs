using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace real_time_horror_group4;

public class Get(HttpListenerRequest request, NpgsqlDataSource db)
{

    public string? path = request.Url.AbsolutePath;
    public string? Lastpath = request.Url.AbsolutePath.Split("/").Last();

    public string menu()
    {
        return "Welcome Contestants!\r\n" +
               "What would you like to do:\r\n" +
               "Register or Log in\r\n";


    }


        public string ShowQuestions()
    {
        string qShow = @"SELECT questions FROM questions";
        string result = string.Empty;

        using (var reader = db.CreateCommand(qShow).ExecuteReader())
        {
            while (reader.Read())
            {
                result += reader.GetString(0) + "\n";
            }
        }

            return result;
        }

    public string users()
    {
        string qusers = @"SELECT username, password FROM users";
        string result = string.Empty;

        using (var reader = db.CreateCommand(qusers).ExecuteReader())
        {
            while (reader.Read()) 
            {
                result += reader.GetString(0) + ", ";
               
                    result += reader.GetString(1) + ":";


            }

        }
        return result;
    }
    public string leaderboard()
    {
        string qboard = @"SELECT wins, losses, userid FROM leaderboard";
        string result = string.Empty;

        using (var reader = db.CreateCommand(qboard).ExecuteReader())
        {
           
            
            while (reader.Read())
            {
                 result += reader.GetInt32(0)  + " wins "; 
              
                result += reader.GetInt32(1)  + " losses ";
                
                result += reader.GetInt32(2)  + " userid ";
            

            }
        }

        return result ;
    }

    public string Getter()
    {
        Console.WriteLine(path);
        if (path != null)
        {
            if (path.Contains("/menu"))
            {
                return menu();
            }
            if (path.Contains("/questions"))
            {
                return ShowQuestions();
            }
            if (path.Contains("/leaderboard"))
            {
                return leaderboard();
            }
            if (path.Contains("/users"))
            {
                
                
                return users();
            }

            

        }
        return "Not Found";
    }

}
