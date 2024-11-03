//using DatabaseWorker;
//using DataModels;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System.Collections.Concurrent;
//using System.Net.Sockets;
//using System.Net.WebSockets;
//using System.Text;

//namespace MessegnerBackend.Models
//{
//    public class WebSocketMiddleware
//    {
//        private WebSocketMiddleware() { }
//        private MembersManager MembersManager = new();

//        private static WebSocketMiddleware _instance = new WebSocketMiddleware();
//        public static WebSocketMiddleware Instance {
//            get
//            {
//                return _instance;
//            }

//        }
//        private ConcurrentDictionary<int, List<WebSocket>> _socketLists = 
//            new();

//        private string Serialize(object data)
//        {
//            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
//            {
//                ContractResolver = new CamelCasePropertyNamesContractResolver()
//            });
//            //return System.Text.Json.JsonSerializer.Serialize(data);
//        }

//        private async Task SendData(WebSocket socket, object data, string type)
//        {
//            string jsonString = Serialize(new
//            {
//                type,
//                data
//            }
//            );

//            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

//             await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
//        }

//        public async Task SendMessage(MessageModel message)
//        {
//            const string type = "message";
//             var users = MembersManager.GetMembers(message.ChatId).Select(user => user.Id).ToList();

//            foreach (var user in users)
//            {
//                await SendData(user, type, message);
                
//            }

//            return;
//        }
//        public async Task RemoveChat(int chatId, int userId)
//        {
//            const string type = "remove chat";

//            //int user = userId;

//            await SendData(userId, type, chatId);
//            return;
//        }
//        public async Task AddChat(int chatId, int userId)
//        {
//            const string type = "add chat";

//            //int user = userId;

//            await SendData(userId, type, chatId);
//            return;
//        }
//        public async Task RemoveMember(int chatId, int memberId)
//        {
//            const string type = "remove member";
//            var users = MembersManager.GetMembers(chatId).Select(user => user.Id).Where(userId => userId != memberId).ToList();

//            await RemoveChat(chatId, memberId);

//            foreach (var user in users)
//            {
//                await SendData(user, type, new
//                {
//                    chatId,
//                    memberId
//                });
//            }

            
//            return;
//        }
//        public async Task SendData(int userId, string type, object data)
//        {
//            if (!_socketLists.Keys.Contains(userId))
//            {

//                return;
//            }
//            List<WebSocket> remove = new(); 

//            foreach (var socket in _socketLists[userId])
//            {
//                if (socket.State == WebSocketState.Closed)
//                {
//                    remove.Add(socket);
//                    continue;
//                }

//                await SendData(socket,
//                    data,
//                type);
//            }
//            foreach (var socket in remove)
//            {
//                _socketLists[userId].Remove(socket);
//            }
//        }
//        public async Task AddMember(int chatId, int memberId)
//        {
//            const string type = "add member";
//            var users = MembersManager.GetMembers(chatId)
//                .Select(user => user.Id)
//                .Where(userId => userId != memberId)
//                .ToList();

//            //await SendData(socket,
//            //            memberId,
//            //        type);
//            await AddChat(chatId, memberId);

//            foreach (var user in users)
//            {
//                await SendData(user, type, new
//                {
//                    chatId, memberId
//                });
//            };

            
//            return;
//        }

//        public void AddSocket(int userId, WebSocket socket)
//        {
//            if (!_socketLists.ContainsKey(userId))
//            {
//                bool d =_socketLists.TryAdd(userId, new List<WebSocket>());

//            }

//            _socketLists[userId].Add(socket);
//        }

//    }
//}
