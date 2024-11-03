//using Dapper;
//using DataModels;
//using System.Data;

//namespace DatabaseWorker
//{
//    public class MessageManager
//    {
//        const string ConnectionIdentifier = "Default";
//        public MessageManager()
//        {
//            _connector = new MysqlConnector(ConnectionIdentifier);
//        }

//        private readonly MysqlConnector _connector;
//        private IDbConnection Connection()
//        {
//            return _connector.Connection();
//        }
//        public MessageModel SendMessage(int SenderId, int ChatId, string Message)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@SenderId", SenderId },
//                    { "@ChatId", ChatId },
//                    { "@Message", Message }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output =
//                db.Query<MessageModel>("call SimpleMessenger.SendMessage(@SenderId,@ChatId,@Message);",
//                parameters).FirstOrDefault();
                
//                return output;
//            }
//        }
//        public List<MessageModel> GetMessages(int ChatId, int userId)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@ChatId", ChatId },
//                    { "@IdUser", userId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output = db.Query<MessageModel>("call SimpleMessenger.GetMessages(@ChatId, @IdUser);",
//                parameters).ToList();
//                return output;
//            }
//        }

//    }
//}
