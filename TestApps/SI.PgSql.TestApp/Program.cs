using Simply.Crud;
using Simply.Data;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SI.PgSql.TestApp
{
    internal class Program
    {
        private static IDbConnection GetDbConnection()
        {
            //return new NpgsqlConnection("server = 127.0.0.1; Database = pagila; user id = postgres; password = 123456;");
            return new NpgsqlConnection("server = 127.0.0.1; Database = postgres; user id = postgres; password = pg123;");
        }

        private static void Main(string[] args)
        {
            using (var connection = GetDbConnection())
            {
                try
                {
                    int id = 0;
                    //var cntr = new Country { CountryName = countryName, LastUpdate = DateTime.Now };
                    connection.OpenIfNot();
                    var result = connection.PartialInsertAndReturnId<Country>(new { CountryName = "Yeni Ülke " + DateTime.Now.Ticks.ToString() });
                    Console.WriteLine(result.ExecutionResult);
                    Console.WriteLine(result.Result);
                    if (result.Result != null)
                    {
                        int.TryParse(result.Result.ToString(), out id);
                    }

                    if (result.AdditionalValues != null)
                    {
                        foreach (var key in result.AdditionalValues.Keys)
                        {
                            Console.WriteLine("{0} : {1}", key, result.AdditionalValues[key]);
                            int.TryParse(result.AdditionalValues[key].ToString(), out id);
                        }
                    }

                    var exec = connection.PartialUpdate<Country>(new { CountryName = "Yeni Ülke22 Güncel " + DateTime.Now.Ticks.ToString(), LastUpdate = DateTime.Now }, p => p.CountryId == id);
                    Console.WriteLine(exec);
                }
                finally
                { connection.CloseIfNot(); }
            }

            Console.ReadKey();
        }
    }

    [Table("country")]
    public class Country
    {
        [Key]
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