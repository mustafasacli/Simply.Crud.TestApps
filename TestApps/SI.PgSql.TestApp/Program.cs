using Simply.Crud;
using Simply.Data.Interfaces;
using SimplyCrud_TestDb_PostgreSql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SI.PgSql.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ISimpleDatabase database = new SimplePostgreSqlDatabase())
            {
                int id = 0;
                var result = database.PartialInsertAndReturnId<Country>(new { CountryName = "Yeni Ülke " + DateTime.Now.Ticks.ToString() });
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

                var exec = database.PartialUpdate<Country>(new { CountryName = "Yeni Ülke22 Güncel " + DateTime.Now.Ticks.ToString(), LastUpdate = DateTime.Now }, p => p.CountryId == id);
                Console.WriteLine(exec);
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