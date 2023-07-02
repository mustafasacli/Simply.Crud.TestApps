using MySql.Data.MySqlClient;
using Simply.Common;
using Simply.Crud;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using SimplyCrud.Project.Entity;
using System;
using System.Linq;

namespace CrudDeleteConsoleApp
{
    internal class Program
    {
        private static readonly string connectionString = "data source=127.0.0.1;initial catalog=crud_test_db;user id=root;";

        private static void Main(string[] args)
        {
            long id = args != null && args.Any() ? args[0].ToLong() : 0;
            int result;
            using (ISimpleDatabase database = new SimpleDatabase(
                SimpleDatabase.Create<MySqlConnection>(connectionString)))
            {
                result = database.DeleteAll<Person>(q => q.Id == id);
            }

            Console.WriteLine($"result: {result}");
            Console.ReadKey();
        }
    }
}