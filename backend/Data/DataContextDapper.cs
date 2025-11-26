using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SAConstruction
{
    public class DataContextDapper
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config["sql-connection-string"];
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            using IDbConnection dbConnection =
            new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            using IDbConnection dbConnection =
            new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            using IDbConnection dbConnection =
            new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            using IDbConnection dbConnection =
            new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    }
}
