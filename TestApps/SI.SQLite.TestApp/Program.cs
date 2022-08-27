using Simply.Crud;
using Simply.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading;

namespace SI.SQLite.TestApp
{
    class Program
    {
        private static string dbFilePath = @"Data Source=TestSqliteDatabase.db3";
        internal static readonly string LocalConnectionString = "Data Source=localDB.s3db;";
        internal static readonly string LocalV3ConnectionString = "Data Source=localDB.s3db;Version=3;Read Only=False;";

        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            var dt2 = now.Date.AddHours(2);

            Console.WriteLine(dt2.ToString("yyyy-MM-dd HH:mm:ss"));

            Stopwatch sw = new Stopwatch();
            using (var connection = new SQLiteConnection() { ConnectionString = dbFilePath })
            {
                connection.OpenIfNot();
                var record = new TestTable();
                record.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                sw.Start();
                var i = connection.InsertAndGetId(record);

                Console.WriteLine("ID: " + record.Id);
                record.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                connection.UpdateInternal(record);
                Thread.Sleep(1000);
                connection.DeleteInternal(record);
                sw.Stop();
                Thread.Sleep(1000);
                connection.CloseIfNot();
            }
            Console.ReadKey();
        }

        class TestTable
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long Id
            { get; set; }

            public string Name
            { get; set; }
        }
    }
}
