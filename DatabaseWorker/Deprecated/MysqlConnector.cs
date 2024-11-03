//using MySql.Data.MySqlClient;
//using System.Configuration;
//using System.Data;

//namespace DatabaseWorker
//{
//    internal class MysqlConnector
//    {
//        private readonly string _connectionString;
//        public MysqlConnector(string connectionStringIdentifier = "Default")
//        {
//            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringIdentifier]?.ConnectionString;
//        }
//        public IDbConnection Connection()
//        {
//            return new MySqlConnection(_connectionString);
//        }
//    }
//}
