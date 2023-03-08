using MySql.Data.MySqlClient;
using Simply.Crud.DatabaseExtensions;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using SimplyCrud.Project.Entites;
using SimplyCrud.Project.Entity;
using System;

namespace CrudInsertAndReturnIdConsoleApp
{
    internal class Program
    {
        private static readonly EntityBuilder entityBuilder = new EntityBuilder();

        //internal static IDbConnection GetDbConnection()
        //{
        //    return new MySqlConnection { ConnectionString = "data source=127.0.0.1;initial catalog=crud_test_db;user id=root;" };
        //}
        private static void Main(string[] args)
        {
            Person person = entityBuilder.CreatePersonObject();
            object result;
            using (ISimpleDatabase database = new SimpleDatabase(
                SimpleDatabase.Create<MySqlConnection>("data source=127.0.0.1;initial catalog=crud_test_db;user id=root;")))
            {
                try
                {
                    result = database.InsertAndGetId(person);
                }
                finally
                { database.Close(); }
            }
            Console.WriteLine(result?.ToString() ?? "#null#");
            Console.Read();
        }
    }
}