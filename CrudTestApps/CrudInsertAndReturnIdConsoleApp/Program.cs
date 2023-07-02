using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Simply.Data;
using System.Collections;
using SimplyCrud.Project.Entites;
using SimplyCrud.Project.Entity;
using Simply.Crud;
using Simply.Data.Interfaces;
using Simply.Data.Database;

namespace CrudInsertAndReturnIdConsoleApp
{
    internal class Program
    {
        private static readonly EntityBuilder entityBuilder = new EntityBuilder();
        private static readonly string connectionString = "data source=127.0.0.1;initial catalog=crud_test_db;user id=root;";

        static void Main(string[] args)
        {
            Person person = entityBuilder.CreatePersonObject();
            object result;
            using (ISimpleDatabase database = new SimpleDatabase(
                SimpleDatabase.Create<MySqlConnection>(connectionString)))
            {
                var objResult = database.InsertAndGetId(person);
                result = objResult.Result;
            }

            Console.WriteLine(result?.ToString() ?? "#null#");
            Console.ReadKey();
        }
    }
}
