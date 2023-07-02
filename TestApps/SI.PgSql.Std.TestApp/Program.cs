using Simply.Crud;
using Simply.Crud.DatabaseExtensions;
using Simply.Data.Interfaces;
using SimplyCrud_TestDb_PostgreSql;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SI.PgSql.Std.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var cntr = new Country { CountryName = "Pagila Country", LastUpdate = DateTime.Now };
            using (ISimpleDatabase database = new SimplePostgreSqlDatabase())
            {
                var result = database.InsertAndGetId(cntr);
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }

    [Table("country")]
    internal class Country
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