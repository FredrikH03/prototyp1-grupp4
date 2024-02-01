using System.Net;

bool listen = true;

Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
{
  Console.WriteLine("interrupting");
  e.Cancel = true;
  listen = false;
};

int port = 3000;
HttpListener listener = new();

listener.Prefixes.Add($"http://127.0.0.1:{port}/");

try
{
  listener.Start();
  listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);
  Console.WriteLine($"server listening to port {port}");
  while (listen) { }
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

    HttpListenerResponse response = context.Response; 
    response.OutputStream.Close();
  }
}

void Router(HttpListenerContext context)
{

  HttpListenerRequest request = context.Request; 
  string path = request.Url?.AbsolutePath ?? "/";
    

  HttpListenerResponse response = context.Response;

  switch (request.HttpMethod, request.Url?.AbsolutePath) 
  {
    case ("GET", "/"):
      Console.WriteLine("test1");
      break;
    
    case ("GET", "/hello"):
      Console.WriteLine("test2");
      break;

    case ("POST", "/"):
      Console.WriteLine("test3");
      break;

    default:
      Console.WriteLine("test4");
      break;

  }
}
