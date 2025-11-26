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
            _connectionString = _config["sql-connection-string"] ?? throw new Exception("Connection string 'sql-connection-string' is missing.");

        }

        public IEnumerable<T> LoadData<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection =
                new SqlConnection(_connectionString);

            return dbConnection.Query<T>(sql, parameters);
        }

        public T LoadDataSingle<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection =
                new SqlConnection(_connectionString);

            return dbConnection.QuerySingle<T>(sql, parameters);
        }

        public bool ExecuteSql(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection =
                new SqlConnection(_connectionString);

            return dbConnection.Execute(sql, parameters) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection =
                new SqlConnection(_connectionString);

            return dbConnection.Execute(sql, parameters);
        }
    }
}
