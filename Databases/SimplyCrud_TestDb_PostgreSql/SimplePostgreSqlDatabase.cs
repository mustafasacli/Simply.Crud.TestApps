using Npgsql;
using Simply.Data.Database;
using System.Data;

namespace SimplyCrud_TestDb_PostgreSql
{
    public class SimplePostgreSqlDatabase : SimpleDatabase
    {
        internal static IDbConnection GetDbConnection()
        {
            return new NpgsqlConnection("server=127.0.0.1;Database=pagila;user id=postgres;password=123456;");
            // return new NpgsqlConnection("server = 127.0.0.1; Database = postgres; user id = postgres; password = pg123;");
        }

        public SimplePostgreSqlDatabase() : base(GetDbConnection())
        {
        }
    }
}