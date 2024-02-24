using Npgsql;
using real_time_horror_group4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class Post
{
    private NpgsqlDataSource db;
    private HttpListenerRequest request;
    private HttpListenerResponse response;

    public Post(NpgsqlDataSource db, HttpListenerRequest request, HttpListenerResponse response)
    {
        this.db = db;
        this.request = request;
        this.response = response;
    }
    public void Pathway(string body)
    {
        string path = request.Url?.AbsolutePath;

        if (path != null)
        {
            if (path.Contains("register"))
            {
                string result = Register(body); // result får det som returnas av metoden
                                                // som sen skrivs ut

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();

            }
            else if (path.Contains("login"))
            {
                string[] userinfo = body.Split(",");
                string username = userinfo[0];
                string password = userinfo[1];

                bool loginSuccessful = ValidateLogin(username, password);
                string result = loginSuccessful ? "You are now logged in, To join a game: curl -X GET localhost:3000/questions " : "Invalid username or password, Curl again";
                SendResponse(result);

            }
            else if (path.Contains("answer"))
            {
                AnswerHandler getquestions = new AnswerHandler(db);

                string[] userAnswer = body.Split(",");
                string userId = userAnswer[0];
                string answeroption = userAnswer[1];
                string questionId = userAnswer[2];



                getquestions.AnswerQuestion(userId,answeroption,questionId);

                string result = getquestions.IsCorrectAnswer ? "Correct! " : "Wrong answer ";
                SendResponse1(result);

            }
            else if (path.Contains("startmatch"))
            {
                GameHandler gameHandler = new GameHandler(db);
                string result = gameHandler.StartMatch(body);
                SendResponse2(result);
            }


        }
        else
        {
            NotFound();
        }
    }

    public string Register(string body)
    {

        string[] user = body.Split(",");
        string username = user[0];
        string password = user[1];

        string Uinsert = @"INSERT INTO users(username,password) VALUES ($1, $2)";

        using var cmd = db.CreateCommand(Uinsert);
        cmd.Parameters.AddWithValue(username);
        cmd.Parameters.AddWithValue(password);
        cmd.ExecuteNonQuery();

        return $"Username:{username} Password: {password} Registered!";
    }
    public bool ValidateLogin(string username, string password)
    {
        string query = @"SELECT * FROM users WHERE username = @username AND password = @password";
        using var cmd = db.CreateCommand(query);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);

        using (var reader = cmd.ExecuteReader())
        {
            int count = 0;
            while (reader.Read())
            {
                count++;

            }
            return count > 0;
        }
    }

    private void SendResponse1(string result)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
        response.ContentType = "text/plain";
        response.StatusCode = (int)HttpStatusCode.OK;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }


    private void SendResponse(string result)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
        response.ContentType = "text/plain";
        response.StatusCode = (int)HttpStatusCode.OK;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
    
    private void SendResponse2(string result)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
        response.ContentType = "text/plain";
        response.StatusCode = (int)HttpStatusCode.OK;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
    
    public void NotFound()
    {
        response.StatusCode = (int)HttpStatusCode.NotFound;
        response.Close();

    }
}


// skillnad på listener.start och begingetcontext
// fråga om streamreader
// fråga om de inbbyggda classerna och context
//fråga om när en class har parametrar
// fråga om hur jag kan returna en ny meny när inloggad