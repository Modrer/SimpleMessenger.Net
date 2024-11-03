using MessegnerBackend.Models.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace MessegnerBackend.Controllers
{
    [ApiController]
    public class SocketController(IUserGetter userGetter) : Controller
    {
        private readonly NotificationManager _notificationSender = NotificationManager.GetInstance();
        private readonly IUserGetter _userGetter = userGetter;

        private Channel<Notification> _channel = Channel.CreateUnbounded<Notification>();

        private async void Notify(Notification notification)
        {
            await _channel.Writer.WriteAsync(notification);
        }

        [Route("/")]
        
        public async Task Get(IHostApplicationLifetime lifetime)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var token = HttpContext.Request.Query["token"];

            var user = _userGetter.GetUser(token);

            if(user == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var close = lifetime.ApplicationStopping;

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            _notificationSender.AddReceiver(user.Id, Notify);

            var buffer = new byte[1024 * 4];

            var readerTask =
                _channel.Reader.ReadAsync().AsTask();

            var socketTask = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), close);

            while (true)
            {
                var result = await Task.WhenAny(readerTask, socketTask);

                if (result == readerTask)
                {
                    Notification notification = readerTask.Result;

                    var message = JsonSerializer.Serialize(notification);
                    var bytes = Encoding.UTF8.GetBytes(message);
                    var arraySegment = new ArraySegment<byte>(bytes);

                    await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, close);

                    readerTask = _channel.Reader.ReadAsync().AsTask();
                    continue;
                }
                if (result == socketTask)
                {
                    if (socketTask.Result.CloseStatus.HasValue)
                    {

                        var closingTask = webSocket
                            .CloseAsync(socketTask.Result.CloseStatus.Value, socketTask.Result.CloseStatusDescription, close);

                        _notificationSender.RemoveReceiver(user.Id, Notify);

                    }

                    var message = "You aren`t allowed send here!";
                    var bytes = Encoding.UTF8.GetBytes(message);
                    var arraySegment = new ArraySegment<byte>(bytes);

                    await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, close);

                    socketTask = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), close);
                    continue;
                }
            }
            

            
        }
        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
