using Simply.Crud;
using Simply.Data;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SI.PgSql.Std.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var connection = GetDbConnection())
            {
                var cntr = new Country { CountryName = "Pagila Country", LastUpdate = DateTime.Now };
                connection.OpenIfNot();
                var result = connection.InsertAndGetId(cntr);
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }

        private static IDbConnection GetDbConnection()
        {
            return new NpgsqlConnection("server = 127.0.0.1; Database = pagila; user id = postgres; password = 123456;");
        }
    }

    [Table("country")]
    class Country
    {
        [Column("country_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId
        { get; set; }


        [Column("country", Order = 2)]
        public string CountryName
        { get; set; }

        [Column("last_update", Order = 2)]
        public DateTime LastUpdate
        { get; set; }
    }
}
