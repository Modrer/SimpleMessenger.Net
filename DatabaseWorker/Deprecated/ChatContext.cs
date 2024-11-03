//using Dapper;
//using DataModels;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

//namespace DatabaseWorker
//{
//    public class ChatContext : DbContext
//    {
//        const string ConnectionIdentifier = "Default";
//        public ChatContext() : base(ConnectionIdentifier)
//        {
//           // _connector = new MysqlConnector(ConnectionIdentifier);
//        }
//        public DbSet<ChatModel> Chats { get; set; }


//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//            modelBuilder.Entity<ChatModel>().MapToStoredProcedures();
//        }

//        //private readonly MysqlConnector _connector;
//        //private IDbConnection Connection()
//        //{
//        //    return _connector.Connection();
//        //}
//        //public ChatModel CreateChat(string name, int ownerId)
//        //{

//        //    using (IDbConnection db = Connection())
//        //    {
//        //        var dictionary = new Dictionary<string, object>
//        //        {
//        //            { "@name", name },
//        //            { "@id", ownerId },
//        //        };
//        //        var parameters = new DynamicParameters(dictionary);

//        //        var output = db.Query<ChatModel>("call SimpleMessenger.CreateChat(@name, @id);",
//        //        parameters).FirstOrDefault();

//        //        return output;
//        //    }
//        //}
//        //public void DeleteChat(int chatId, int ownerId)
//        //{

//        //    using (IDbConnection db = Connection())
//        //    {
//        //        var dictionary = new Dictionary<string, object>
//        //        {
//        //            { "@chatId", chatId },
//        //            { "@id", ownerId },
//        //        };
//        //        var parameters = new DynamicParameters(dictionary);

//        //        db.Execute("call SimpleMessenger.DeleteChat(@chatId, @id);",
//        //        parameters);
//        //    }
//        //}
//        ////////public void GetChat(int chatId, int ownerId)
//        ////////{

//        ////////    using (IDbConnection db = Connection())
//        ////////    {
//        ////////        var dictionary = new Dictionary<string, object>
//        ////////        {
//        ////////            { "@chatId", chatId },
//        ////////            { "@id", ownerId },
//        ////////        };
//        ////////        var parameters = new DynamicParameters(dictionary);

//        ////////        db.Execute("call SimpleMessenger.DeleteChat(@chatId, @id);",
//        ////////        parameters);
//        ////////    }
//        ////////}
//        //public List<ChatModel> GetChats(int IdUser)
//        //{

//        //    using (IDbConnection db = Connection())
//        //    {
//        //        var dictionary = new Dictionary<string, object>
//        //        {
//        //            { "@IdUser", IdUser }
//        //        };
//        //        var parameters = new DynamicParameters(dictionary);

//        //        Console.WriteLine(db.ConnectionString);

//        //        var output = db
//        //            .Query<ChatModel>("call SimpleMessenger.GetAllChats(@IdUser);",
//        //        parameters)
//        //            .ToList();

//        //        return output;
//        //    }
//        //}
//        ////TODO if nessesary
//        ////public void searchchat(int chatid, int ownerid)
//        ////{

//        ////    using (idbconnection db = connection())
//        ////    {
//        ////        var dictionary = new dictionary<string, object>
//        ////        {
//        ////            { "@chatid", chatid },
//        ////            { "@id", ownerid },
//        ////        };
//        ////        var parameters = new dynamicparameters(dictionary);

//        ////        db.execute("call simplemessenger.deletechat(@chatid, @id);",
//        ////        parameters);
//        ////    }
//        ////}
//    }
//}
