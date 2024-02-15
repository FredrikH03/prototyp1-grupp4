using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace real_time_horror_group4;

public class Get(HttpListenerRequest request, NpgsqlDataSource db)
{
    public string? path = request.Url.AbsolutePath;
    public string? Lastpath = request.Url.AbsolutePath.Split("/").Last();

    public string test()
    {

        string test = "test";

        return test;

    }
     
    public string getter()// vi kör denna typen av metoden för vi vill returna string, void returnar inte.
    {
        if(path != null)
        {
            if (path.Contains("/test"))
            {
                return test(); 


            }
            if (path.Contains("/leaderboard"))
            {


                return leaderboard(); // här vill vi return senaste metoden.


            }
            if (path.Contains("/User"))
            {
                return user();
            }



        }



        return "not found";

    }


     
    

    public string leaderboard()
    {
        string qleaderboard = @"SELECT * FROM leaderboard);";

        return qleaderboard;




    }

    public string user()
    {
        string qUser = @" 
        SELECT * FROM users
        ;";

       return qUser;

    }


}
