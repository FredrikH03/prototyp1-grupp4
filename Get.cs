using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

    public string ShowQuestion1() //spelare1 hämtar från denna
    {
        List<string> questionList = new List<string>();

        string qFetch = @"SELECT questions FROM questions";

        using (var reader = db.CreateCommand(qFetch).ExecuteReader())
        {
            while (reader.Read())
            {
                string question = reader.GetString(0);
                questionList.Add(question);
            }
        }

        return questionList[0];

    }

    public string ShowQuestion2() //spleare2 hämtar från denna
    {
        List<string> questionList = new List<string>();

        string qFetch = @"SELECT questions FROM questions";

        using (var reader = db.CreateCommand(qFetch).ExecuteReader())
        {
            while (reader.Read())
            {
                string question = reader.GetString(0);
                questionList.Add(question);
            }
        }

        return questionList[0];

    }


      public string Getter()
    {
        if (path != null)
        {
            if (path.Contains("/menu"))
            {
                return menu();
            }
            if (path.Contains("/questions1"))
            {
                return ShowQuestion1();
            }
            if (path.Contains("/questions2"))
            {
                return ShowQuestion2();
            }

        }
        return "Not Found";
    }

    
}
