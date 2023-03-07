using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Configuration;
using Simply.Common;
using Simply.Data.Interfaces;
using SimplyCrud_TestDb_MySql;
using Simply.Crud.DatabaseExtensions;

namespace BulkInsertAndReturnIdConsoleApp
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
            int entityCountForInsertAndReturnId =
                ConfigurationManager.AppSettings["entityCountForInsertAndReturnId"].ToIntNullable() ?? 0;

            if (entityCountForInsertAndReturnId < 100)
                entityCountForInsertAndReturnId = 100;

            if (entityCountForInsertAndReturnId > 10000)
                entityCountForInsertAndReturnId = 1000000;
            List<Person> persons = new List<Person>();
            for (int counter = 0; counter < entityCountForInsertAndReturnId; counter++)
            {
                Person person = entityBuilder.CreatePersonObject();
                persons.Add(person);
            }

            IDbCommandResult<object[]> result;
            using (ISimpleDatabase connection = new SimpleMySqlDatabase())
            {
                try
                {
                    result = connection.InsertAndGetId(persons);
                }
                finally
                { connection.Close(); }
            }

            Console.WriteLine("ExecutionResult: " + result?.ExecutionResult);
            foreach (var key in result.AdditionalValues.Keys)
            {
                Console.WriteLine("Key: #{0}#, Value: #{1}#", key, result.AdditionalValues[key]);
            }

            for (int counter = 0; counter < result.Result.Length; counter++)
            {
                Console.WriteLine("Value for({0}): #{1}#", counter, result.Result[counter]);
            }

            Console.Read();
        }
    }
}
