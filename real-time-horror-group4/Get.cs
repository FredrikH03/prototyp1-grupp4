using Npgsql;
using real_time_horror_group4;
using System.Net;

public class Get(HttpListenerContext context, NpgsqlDataSource db)
{
    public string? path = context.Request.Url.AbsolutePath;
    public string? Lastpath = context.Request.Url.AbsolutePath.Split("/").Last();

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

    public string Getter()
    {
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
            else
            {
                Authenticator authenticator = new Authenticator();
                authenticator.Authenticator(context, db);
            }

        }
        return "Not Found";
    }
}