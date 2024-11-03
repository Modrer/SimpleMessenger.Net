//using Dapper;
//using DataModels;
//using System.Data;

//namespace DatabaseWorker
//{
//    public class ContactsManager
//    {
//        const string ConnectionIdentifier = "Default";
//        public ContactsManager()
//        {
//            _connector = new MysqlConnector(ConnectionIdentifier);
//        }

//        private readonly MysqlConnector _connector;
//        private IDbConnection Connection()
//        {
//            return _connector.Connection();
//        }

//        public void AddContact(int ownerId, int contactId)
//        {
//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@ownerId", ownerId },
//                    {  "@contactId" , contactId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                db.Execute("call SimpleMessenger.AddContact(@ownerId,@contactId);",
//                parameters);

//                return;
//            }
//        }
//        public void RemoveContact(int ownerId, int contactId)
//        {
//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@ownerId", ownerId },
//                    {  "@contactId" , contactId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                db.Execute("call SimpleMessenger.RemoveContact(@ownerId,@contactId);",
//                parameters);

//                return;
//            }
//        }
//        public List<ContactModel> GetContacts(int ownerId)
//        {
//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@ownerId", ownerId }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output = db
//                    .Query<ContactModel>("call SimpleMessenger.GetAllContacts(@ownerId);",
//                parameters)
//                    .ToList();

//                return output;
//            }
//        }
//        public List<ContactModel> SearchContact(int ownerId, string contactName)
//        {
//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@ownerId", ownerId },
//                    { "@contactName", contactName }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output = db.Query<ContactModel>("call SimpleMessenger.FindContactsByName(@contactName,@ownerId);",
//                parameters).ToList();

//                return output;
//            }
//        }
//    }
//}
