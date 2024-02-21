using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Npgsql;
using real_time_horror_group4;
using System.Diagnostics.Metrics;
using RealHorror4;
using System.Reflection;


string connectionString = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=Horror#4"; // skapa pathen

await using var db = NpgsqlDataSource.Create(connectionString); // kopplingen startar med min databas

Tables table = new Tables(db);
await table.CreateTables();

bool listen = true;

Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e) // med hjälp av detta så kan vi göra listen till false med ctrl C
{
    Console.WriteLine("Interupting cancel event");
    e.Cancel = true;
    listen = false;
};

int port = 3000;

HttpListener listener = new();
listener.Prefixes.Add($"http://127.0.0.1:{port}/"); // URL är adressen den ska lyssna på och porten är vilken dörr den ska lyssna på

try
{
    listener.Start(); // den börjar nu lyssna på vad som finns att komma och vi gör console.writeline för att veta det.
    Console.WriteLine("Listening...");
    listener.BeginGetContext(new AsyncCallback(HandleRequest), listener); // inväntar vad för request som ska komma
                                                                          // för att sedan kalla handlerequest som ska hantera den
    while (listen) { };

}
finally
{
    listener.Stop();
}

void HandleRequest(IAsyncResult result) // förklara mer...
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

    Post p = new Post(db, request, response);

    switch (request.HttpMethod)
    {

        case ("GET"):
            byte[] buffer = Encoding.UTF8.GetBytes(getters.Getter());
            response.ContentType = "text/plain";
            response.StatusCode = (int)HttpStatusCode.OK;

            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            break;
        case ("POST"):
            //läser av bodyn som skrivs i terminalen
            //lägger in det skrivna in i body variable

            StreamReader reader = new(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            p.Pathway(body);

            response.StatusCode = (int)HttpStatusCode.Created;
            response.Close();
            break;
        default:
            NotFound(response);
            break;
    }
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
