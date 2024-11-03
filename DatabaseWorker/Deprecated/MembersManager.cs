//using Dapper;
//using DataModels;
//using System.Data;

//namespace DatabaseWorker
//{
//    public class MembersManager
//    {
//        const string ConnectionIdentifier = "Default";
//        public MembersManager()
//        {
//            _connector = new MysqlConnector(ConnectionIdentifier);
//        }

//        private readonly MysqlConnector _connector;
//        private IDbConnection Connection()
//        {
//            return _connector.Connection();
//        }
//        public List<UserInfo> GetMembers(int chatId)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@chatId", chatId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output =
//                db.Query<UserInfo>("call SimpleMessenger.GetAllMembers(@chatId);",
//                parameters).ToList();

//                return output;
//            }
//        }
//        public void SetReadedMessage(int chatId, int userId, int messageId)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@chatId", chatId },
//                    { "@IdUser", userId },
//                    { "@MessageId", messageId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                db.Execute("call SimpleMessenger.SetLastReadedMessage(@chatId,@IdUser,@MessageId);",
//                parameters);
//            }
//        }
//        public void AddMember(int chatId, int userId)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@chatId", chatId },
//                    { "@IdUser", userId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                db.Execute("call SimpleMessenger.AddChatMember(@chatId,@IdUser );",
//                parameters);
//            }
//        }
//        public void RemoveMember(int chatId, int userId)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@chatId", chatId },
//                    { "@IdUser", userId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                db.Execute("call SimpleMessenger.RemoveChatMember(@chatId,@IdUser );",
//                parameters);
//            }
//        }
//    }
//}
