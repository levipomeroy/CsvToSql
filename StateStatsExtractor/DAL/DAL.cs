using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace StateStatsExtractor.DAL
{
    public class DataAccess
    {
        private readonly string ConnectionString;

        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }

        public bool CreateTable(string sql)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(
                     sql: sql,
                     commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                //error
                return false;
            }

            return true;
        }

        public bool AddRecords(string sql)
        {
            try
            {
                using (var connection = GetConnection())
                {
                   connection.Execute(
                     sql: sql,
                     commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                //error
                return false;
            }

            return true;
        }

    }
}
