using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Npgsql;
using real_time_horror_group4;


string connectionString = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=Horror#4";

await using var db = NpgsqlDataSource.Create(connectionString);

Tables table = new Tables(db);
await table.CreateTables();

InsertInfo insert = new InsertInfo(db);
await insert.PopulateQuestions();
await insert.PopulateAnswers();

InsertInfo insert = new InsertInfo(db);
Console.WriteLine("do you want to populate questions and answers? y/N");
switch (Console.ReadLine())
{
    case "y":
        await insert.PopulateQuestions();
        await insert.PopulateAnswers();
        break;
    default:
        break;
}



bool listen = true;

/// Handle ctrl + c interup event, and gracefully shut down server
Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
{
    Console.WriteLine("Interupting cancel event");
    e.Cancel = true;
    listen = false;
};

int port = 3000;

HttpListener listener = new();
listener.Prefixes.Add($"http://127.0.0.1:{port}/"); // <host> kan t.ex. vara 127.0.0.1, 0.0.0.0, ...

try
{
    listener.Start();
    Console.WriteLine("Listening...");
    listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);
    while (listen) { };

}
finally
{
    listener.Stop();
}

void HandleRequest(IAsyncResult result)
{
    if (result.AsyncState is HttpListener listener)
    {
        HttpListenerContext context = listener.EndGetContext(result);
        Router(context);


        listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);
    }

}


    void Router(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        Get getters = new Get(request, db);


        switch (request.HttpMethod)
        {

            case ("GET"):
                byte[] buffer = Encoding.UTF8.GetBytes(getters.getter());
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.OK;

                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                break;
            case ("POST"):
                RootPost(request, response);
                break;
            default:
                NotFound(response);
                break;
        }
    }

    void RootGet(HttpListenerResponse response)
    {
        string message = "test"; // byt ut till vilken text som ska skickas tillbaka
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        response.ContentType = "text/plain";
        response.StatusCode = (int)HttpStatusCode.OK;

        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }



    void Leaderboard(HttpListenerResponse response)
    {
        string message = "hello"; // byt ut till vilken text som ska skickas tillbaka
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        response.ContentType = "text/plain";
        response.StatusCode = (int)HttpStatusCode.OK;

        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }

    void RootPost(HttpListenerRequest req, HttpListenerResponse res)
    {
        StreamReader reader = new(req.InputStream, req.ContentEncoding);
        string body = reader.ReadToEnd();

        // metod här för att hantera request body 
        Console.WriteLine($"Created the following in db: {body}");

        res.StatusCode = (int)HttpStatusCode.Created;
        res.Close();
    }


    void NotFound(HttpListenerResponse res)
    {
        res.StatusCode = (int)HttpStatusCode.NotFound;
        res.Close();
    }


