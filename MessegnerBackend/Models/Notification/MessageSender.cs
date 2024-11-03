using DatabaseWorker;
using DatabaseWorker.FlatModels;
using System.Text.Json;
using static MessegnerBackend.Models.Notification.NotificationManager;

namespace MessegnerBackend.Models.Notification
{
    public static class MessageSender
    {
        private const string s_MESSAGE = "message";

        private const string s_ADDMEMBER = "add member";
        private const string s_REMOVEMEMBER = "remove member";
                
        private const string s_ADDCHAT = "add chat";
        private const string s_REMOVECHAT = "remove chat";
        private const string s_UPDATECHAT = "update chat";
        
        private static readonly NotificationManager s_sender = NotificationManager.GetInstance();

        #region Chat
        private static string ChatMessageTypeToString(ChatMessageType type)
        {
            return type switch
            {
                ChatMessageType.addChat => s_ADDCHAT,
                ChatMessageType.removeChat => s_REMOVECHAT,
                ChatMessageType.updateChat => s_UPDATECHAT,
                _ => throw new Exception("Wrong message type"),
            };
            ;
            
        }
        public static void SendDeleteChat(IEnumerable<int> members, int chatId)
        {
            s_sender.Send(members,
                new Notification
                {
                    Type = s_REMOVECHAT,
                    Message = chatId.ToString()
                }
            );
        }
        public static void SendDeleteChat(int userId, int chatId)
        {
            s_sender.Send(userId,
                new Notification
                {
                    Type = s_REMOVECHAT,
                    Message = chatId.ToString()
                }
            );
        }
        public static void SendChat(int userId, FlatChat chat, ChatMessageType type)
        {
            string message = JsonSerializer.Serialize(chat);

            s_sender.Send(userId,
                new Notification
                {
                    Type = ChatMessageTypeToString(type),
                    Message = message
                }
            );
        }
        public static void SendChat(IEnumerable<int> userIds, FlatChat chat, ChatMessageType type)
        {
            string message = JsonSerializer.Serialize(chat);

            s_sender.Send(userIds,
                new Notification
                {
                    Type = ChatMessageTypeToString(type),
                    Message = message
                }
            );
        }
        #endregion Chat

        #region Members
        private static string MembersMessageTypeToString(MembersMessageType type)
        {
            return type switch
            {
                MembersMessageType.addMember => s_ADDMEMBER,
                MembersMessageType.removeMember => s_REMOVEMEMBER,
                _ => throw new Exception("Wrong message type"),
            };
            ;

        }
        public static void SendMember(int userId, int chatId, int memberId, MembersMessageType type)
        {
            string message = JsonSerializer.Serialize(
                new MemberChange { ChatId = chatId, UserId = memberId }
                );

            s_sender.Send(userId,
                new Notification
                {
                    Type = MembersMessageTypeToString(type),
                    Message = message
                }
            );
        }
        public static void SendMember(IEnumerable<int> userIds, int chatId, int memberId, MembersMessageType type)
        {
            string message = JsonSerializer.Serialize(
                new MemberChange { ChatId = chatId, UserId = memberId }
                );

            s_sender.Send(userIds,
                new Notification
                {
                    Type = MembersMessageTypeToString(type),
                    Message = message
                }
            );
        }
        #endregion Members

        #region Message
        public static void SendMessage(IEnumerable<int> userIds, FlatMessage message)
        {
            string messageString = JsonSerializer.Serialize(message);

            s_sender.Send(userIds,
                new Notification
                {
                    Type = s_MESSAGE,
                    Message = messageString
                }
            );
        }
        #endregion Message

    }
}
