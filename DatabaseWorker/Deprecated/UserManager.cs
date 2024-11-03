//using Dapper;
//using DataModels;
//using System.Data;

//namespace DatabaseWorker
//{
//    public class UserManager
//    {
//        const string ConnectionIdentifier = "Default";
//        public UserManager()
//        {
//            _connector = new MysqlConnector(ConnectionIdentifier);
//        }

//        private readonly MysqlConnector _connector;
//        private IDbConnection Connection()
//        {
//            return _connector.Connection();
//        }
//        public UserModel GetUser(string userName)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@userName", userName }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output =
//                db.Query<UserModel>("call SimpleMessenger.FindUserByName(@userName);",
//                parameters).ToList();

//                return output.FirstOrDefault();
//            }
//        }
//        public UserModel GetUser(int id)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@id", id }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output =
//                db.Query<UserModel>("call SimpleMessenger.FindUserById(@id);",
//                parameters).ToList();

//                return output.FirstOrDefault();
//            }
//        }
//        public List<UserModel> SearchUser(string userName)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@userName", userName }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                //var output =
//                //db.Query<UserModel>("Select id, Name, Email, Password as PasswordHash, Salt, Role from Users where STRCMP(Name,@userName) = 0",
//                //parameters).ToList();


//                var output =
//                db.Query<UserModel>("call SimpleMessenger.FindUsersByName(@userName);",
//                parameters).ToList();

//                return output;
//            }
//        }
//        public bool ContainsUser(string userName)
//        {

//            using (IDbConnection db = Connection())
//            {
//                var dictionary = new Dictionary<string, object>
//                {
//                    { "@userName", userName }
//                };
//                var parameters = new DynamicParameters(dictionary);

//                var output =
//                db.Query<string>("Select 1 from Users where Name = @userName",
//                parameters).ToList();
//                return output.Count == 0;
//            }
//        }
//        public List<UserModel> GetUsers()
//        {
//            using (IDbConnection db = Connection())
//            {

//                var output =
//                db.Query<UserModel>("Select id as UserId, Name, Email ,Password, Salt, Role from Users", new DynamicParameters()).ToList();
//                return output;
//            }
//        }
//        public void AddUser(UserModel user)
//        {
//            using (var db = Connection())
//            {

//                db.Execute(@"INSERT INTO Users(Name, Email ,Password, Salt, Role) 
//                VALUES(@Name,@Email,@Password,@Salt,@Role)",
//                user);
//            }
//        }
//    }
//}
