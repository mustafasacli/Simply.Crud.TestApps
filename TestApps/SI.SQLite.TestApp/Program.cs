using Simply.Crud;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading;

namespace SI.SQLite.TestApp
{
    internal class Program
    {
        private static string dbFilePath = @"Data Source=D:\GitProjects\Simply.Crud.TestApps\TestApps\SI.SQLite.TestApp\TestSqliteDatabase.db3;Version=3;";
        internal static readonly string LocalConnectionString = "Data Source=localDB.s3db;";
        internal static readonly string LocalV3ConnectionString = "Data Source=localDB.s3db;Version=3;Read Only=False;";

        private static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            var dt2 = now.Date.AddHours(2);

            Console.WriteLine(dt2.ToString("yyyy-MM-dd HH:mm:ss"));

            Stopwatch sw = new Stopwatch();
            using (ISimpleDatabase database = new SimpleDatabase(SimpleDatabase.Create<SQLiteConnection>(dbFilePath)))
            {
                //database.AutoClose = true;
                var record = new TestTable();
                record.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                sw.Start();
                var returnValues = database.InsertAndGetId(record);
                sw.Stop();
                Console.WriteLine("Execution time(msec) : " + sw.ElapsedMilliseconds);
                Console.WriteLine("Result: " + returnValues.Result);
                Console.WriteLine("ExecutionResult: " + returnValues.ExecutionResult);
                if (returnValues.AdditionalValues != null)
                {
                    foreach (var item in returnValues.AdditionalValues.Keys)
                    {
                        Console.WriteLine("Key: " + returnValues.AdditionalValues[item]);
                    }
                }
                Console.WriteLine("-------------------");
                Console.WriteLine("ID: " + record.Id);
                record.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                sw.Reset();
                sw.Start();
                int updateResult = database.Update(record);
                sw.Stop();
                Console.WriteLine("Execution time(msec) : " + sw.ElapsedMilliseconds);
                Console.WriteLine("Update Result : " + updateResult);
                Console.WriteLine("-------------------");
                Thread.Sleep(1000);
                sw.Reset();
                sw.Start();
                updateResult = database.Delete(record);
                sw.Stop();
                Console.WriteLine("Delete Result : " + updateResult);
                Console.WriteLine("-------------------");
                Thread.Sleep(1000);
                database.Close();
                Console.WriteLine("Execution time(msec) : " + sw.ElapsedMilliseconds);
            }
            Console.ReadKey();
        }
    }

    internal class TestTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id
        { get; set; }

        public string Name
        { get; set; }
    }
}