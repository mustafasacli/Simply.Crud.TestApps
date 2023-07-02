using Simply.Crud.Enums;
using Simply.Crud.Query;
using Simply.Data;
using Simply.Data.Enums;
using Mst.Dexter.Factory;
using SI.Test.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Simply.Definitor.Attribute;

namespace SI.QueryTestConsoleApp
{
    internal class Program
    {
        // This example assumes a reference to System.Data.Common.
        static DataTable GetProviderFactoryClasses()
        {
            // Retrieve the installed providers and factories.
            DataTable table = DbProviderFactories.GetFactoryClasses();

            // Display each row and column value.
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(column.ColumnName + "---" + row[column]);
                }
            }
            return table;
        }
        private static void Main(string[] args)
        {
            GetProviderFactoryClasses();

            IList<string> keys = DxConnectionFactory.Instance.ConnectionKeys;

            foreach (var key in keys)
            {
                Console.WriteLine($"Key: {key}.");
            }

            Console.WriteLine("**********************************************************");
            IDbConnection conn;
            string q;
            DbConnectionTypes connType;

            foreach (var key in keys)
            {
                Console.WriteLine($"Key: {key}.");
                conn = DxConnectionFactory.Instance.GetConnection(key);

                connType = conn.GetDbConnectionType();
                var queryBuilder = QueryBuilderFactory.GetQueryBuilder(connType);

                Console.WriteLine($"DbConnectionType : {(int)connType}, {connType}.");
                /* ----------------------------------------------- */

                var baseCmd1 = queryBuilder.BuildBaseCommand<LogEntry>(new { UserId = 3, LogTime = DateTime.Now, Title = "sth" }, QueryTypes.InsertAndGetId);
                Console.WriteLine(baseCmd1.CommandText);
                baseCmd1.CommandParameters.ToList().ForEach(p => Console.WriteLine(p));

                AttributeDefinitor<LogEntry> definitor = new Simply.Definitor.Attribute.AttributeDefinitor<LogEntry>();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(typeof(LogEntry).FullName);
                q = definitor.GetTableName();//connType.GetFullTableName<LogEntry>();
                Console.WriteLine($"FullTableName: {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.Insert);
                Console.WriteLine($"BuildInsertQuery : {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.InsertAndGetId);
                Console.WriteLine($"BuildInsertAndGetIdQuery : {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.Update);
                Console.WriteLine($"BuildUpdateQuery : {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.Delete);
                Console.WriteLine($"BuildDeleteQuery : {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.GetById);
                Console.WriteLine($"BuildGetByIdQuery : {q}");
                q = queryBuilder.BuildQuery<LogEntry>(QueryTypes.GetAll);
                Console.WriteLine($"BuildGetAllQuery : {q}");
                /* ----------------------------------------------- */

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(typeof(PersonalFile).FullName);
                AttributeDefinitor<PersonalFile> definitor2 = new Simply.Definitor.Attribute.AttributeDefinitor<PersonalFile>();
                q = definitor2.GetTableName();//connType.GetFullTableName<PersonalFile>();
                Console.WriteLine($"FullTableName: {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.Insert);
                Console.WriteLine($"BuildInsertQuery : {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.InsertAndGetId);
                Console.WriteLine($"BuildInsertAndGetIdQuery : {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.Update);
                Console.WriteLine($"BuildUpdateQuery : {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.Delete);
                Console.WriteLine($"BuildDeleteQuery : {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.GetById);
                Console.WriteLine($"BuildGetByIdQuery : {q}");
                q = queryBuilder.BuildQuery<PersonalFile>(QueryTypes.GetAll);
                Console.WriteLine($"BuildGetAllQuery : {q}");
                /* ----------------------------------------------- */

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(typeof(TransactionLog).FullName);
                AttributeDefinitor<TransactionLog> definitor3 = new Simply.Definitor.Attribute.AttributeDefinitor<TransactionLog>();
                q = definitor3.GetTableName();//
                //q = connType.GetFullTableName<TransactionLog>();
                Console.WriteLine($"FullTableName: {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.Insert);
                Console.WriteLine($"BuildInsertQuery : {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.InsertAndGetId);
                Console.WriteLine($"BuildInsertAndGetIdQuery : {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.Update);
                Console.WriteLine($"BuildUpdateQuery : {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.Delete);
                Console.WriteLine($"BuildDeleteQuery : {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.GetById);
                Console.WriteLine($"BuildGetByIdQuery : {q}");
                q = queryBuilder.BuildQuery<TransactionLog>(QueryTypes.GetAll);
                Console.WriteLine($"BuildGetAllQuery : {q}");
                /* ----------------------------------------------- */

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(typeof(User).FullName);
                AttributeDefinitor<User> definitor4 = new Simply.Definitor.Attribute.AttributeDefinitor<User>();
                q = definitor3.GetTableName();//
                // q = connType.GetFullTableName<User>();
                Console.WriteLine($"FullTableName: {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.Insert);
                Console.WriteLine($"BuildInsertQuery : {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.InsertAndGetId);
                Console.WriteLine($"BuildInsertAndGetIdQuery : {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.Update);
                Console.WriteLine($"BuildUpdateQuery : {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.Delete);
                Console.WriteLine($"BuildDeleteQuery : {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.GetById);
                Console.WriteLine($"BuildGetByIdQuery : {q}");
                q = queryBuilder.BuildQuery<User>(QueryTypes.Delete);
                Console.WriteLine($"BuildGetAllQuery : {q}");
                /* ----------------------------------------------- */

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(typeof(UserType).FullName);
                AttributeDefinitor<UserType> definitor5 = new Simply.Definitor.Attribute.AttributeDefinitor<UserType>();
                q = definitor3.GetTableName();//
                // q = connType.GetFullTableName<UserType>();
                Console.WriteLine($"FullTableName: {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.Insert);
                Console.WriteLine($"BuildInsertQuery : {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.InsertAndGetId);
                Console.WriteLine($"BuildInsertAndGetIdQuery : {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.Update);
                Console.WriteLine($"BuildUpdateQuery : {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.Delete);
                Console.WriteLine($"BuildDeleteQuery : {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.GetById);
                Console.WriteLine($"BuildGetByIdQuery : {q}");
                q = queryBuilder.BuildQuery<UserType>(QueryTypes.GetAll);
                Console.WriteLine($"BuildGetAllQuery : {q}");
                /* ----------------------------------------------- */

                Console.WriteLine("**********************************************************");
            }

            Console.Read();
        }
    }
}
