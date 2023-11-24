using MySql.Data.MySqlClient;
using Simply.Crud;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using SimplyCrud.Project.Entites;
using SimplyCrud.Project.Entity;
using Simply.Crud;

namespace CrudInsertAndReturnIdConsoleApp
{
    internal class Program
    {
        private static readonly EntityBuilder entityBuilder = new EntityBuilder();
        internal static IDbConnection GetDbConnection()
        {
            return new MySqlConnection { ConnectionString = "data source=127.0.0.1;initial catalog=crud_test_db;user id=root;" };
        }
        static void Main(string[] args)
        {
            Person person = entityBuilder.CreatePersonObject();
            object result;
            using (IDbConnection connection = GetDbConnection())
            {
                try
                {
                    result = connection.OpenAnd()
                       .InsertAndGetId(person);
                }
                finally
                { connection.CloseIfNot(); }
            }

            Console.WriteLine(result?.ToString() ?? "#null#");
            Console.ReadKey();
        }
    }
}