using System.Net.WebSockets;
using System.Text;
using System.Threading;

var ws = new ClientWebSocket();

Console.WriteLine("Connecting to server");
await ws.ConnectAsync(new Uri("ws://localhost:6969/post-notification"), CancellationToken.None);
Console.WriteLine("Connected");

var receiveTask = Task.Run(async () =>
{
    var buffer = new byte[1024 * 4];
    while (true) 
    {
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if(result.MessageType == WebSocketMessageType.Close)
        {
            break;
        }

        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine("Received: " + message);
    }
});

await receiveTask;