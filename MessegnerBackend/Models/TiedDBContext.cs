using DatabaseWorker;
using DatabaseWorker.FlatModels;
using MessegnerBackend.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace MessegnerBackend.Models
{
    public class TiedDBContext : SimpleMessengerContext
    {

        public TiedDBContext() : base() {}
        public TiedDBContext(DbContextOptions<TiedDBContext> options)
            : base(new DbContextOptions<SimpleMessengerContext>(
                options.Extensions.ToDictionary(e => e.GetType()))
                  ) {

        }
        #region Chat
        public override FlatChat? CreateChat(int ownerId, string name, string image = "default.png")
        {
            var result = base.CreateChat(ownerId, name, image);

            if (result != null) {
                MessageSender.SendChat(ownerId, result, ChatMessageType.addChat);
            }

            return result;

        }

        

        public override FlatChat? UpdateChat(int chatId, string name, string? image)
        {
            FlatChat? result = base.UpdateChat(chatId, name, image);

            if (result != null)
            {
                var members = base.GetMembers(chatId).Select(x => x.Id);

                MessageSender.SendChat(members, result, ChatMessageType.updateChat);
            }

            return result;

        }

        public override bool DeleteChat(int chatId, int ownerId)
        {
            bool result = base.DeleteChat(chatId, ownerId);

            if (result)
            {
                var members = base.GetMembers(chatId).Select(member => member.Id).AsEnumerable();
                MessageSender.SendDeleteChat(members, chatId);
            }

            return result;

        }

        #endregion Chat

        #region Members
        public override bool AddMember(int chatId, int userId)
        {
            var result = base.AddMember(chatId, userId);
            
            if (!result)
            {
                return result;

            }

            var members = base.GetMembers(chatId).Select(user => user.Id).AsEnumerable();

            MessageSender.SendMember(members, chatId, userId, MembersMessageType.addMember);

            var chat = base.GetChat(chatId);

            if (chat != null)
            {
                MessageSender.SendChat(userId, chat, ChatMessageType.addChat);
            }

            return result;

        }

        public override bool RemoveMember(int chatId, int userId)
        {
            var result = base.RemoveMember(chatId, userId);

            if (!result)
            {
                return result;

            }

            var members = base.GetMembers(chatId).Select(user => user.Id).AsEnumerable();

            MessageSender.SendMember(members, chatId, userId, MembersMessageType.removeMember);

            MessageSender.SendDeleteChat(userId, chatId);

            return result;

        }
        #endregion Members

        #region Message
        public override FlatMessage? SendMessage(int senderId, int chatId, string text)
        {
            var result = base.SendMessage(senderId, chatId, text);

            if (result != null) {
                var members = base.GetMembers(chatId).Select(user => user.Id).AsEnumerable();
                MessageSender.SendMessage(members, result);
            }

            return result;
        }

        #endregion Message
    }
}
