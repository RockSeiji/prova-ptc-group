using Cast;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketServer.controller
{
    public class WebSocketController : ControllerBase
    {
        private readonly CastDBContext _context;

        public WebSocketController(CastDBContext context)
        {
            _context = context;
        }

        [Route("/post-notification")]
        public async Task Get()
        {
            var postsLength = 0;
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (true)
                {
                    var currentPosts = await GetPostsLength();
                    if (currentPosts > postsLength)
                    {
                        postsLength = currentPosts;
                        await webSocket.SendAsync(
                        Encoding.ASCII.GetBytes($"Chegaram novas postagens: {DateTime.Now}"),
                        WebSocketMessageType.Text,
                        true, CancellationToken.None);
                    }
                 
                    await Task.Delay(1000);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task<Int32> GetPostsLength()
        {
            return _context.Posts.Count();
        }
    }
}
