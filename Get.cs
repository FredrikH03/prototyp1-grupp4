using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using real_time_horror_group4;


public class Get(HttpListenerRequest request, NpgsqlDataSource db)
{
    public string? path = request.Url.AbsolutePath;
    public string? Lastpath = request.Url.AbsolutePath.Split("/").Last();

    public string getter()// vi kör denna typen av metoden för vi vill returna string, void returnar inte.
    {
        if(path != null)
        {
            if (path.Contains("/questions"))
            {
                return test().ToString(); 


            }
           
            if (path.Contains("/User"))
            {
                return user();
            }

        }

        return "not found";

    }

    public  string test()
    {

        GetQuestions question = new GetQuestions(db);
        string test = null;
         test = question.GetRandomQuestion();

        return test;

    }

    public string user()
    {
        string qUser = @" 
        SELECT * FROM users
        ;";

       return qUser;

    }

}
