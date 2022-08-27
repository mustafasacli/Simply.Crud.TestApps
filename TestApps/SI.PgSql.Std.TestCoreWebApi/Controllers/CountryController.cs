using Simply.Crud;
using Simply.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace SI.PgSql.Std.TestCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {

        private static IDbConnection GetDbConnection()
        {
            //return new NpgsqlConnection("server = 127.0.0.1; Database = pagila; user id = postgres; password = 123456;");
            return new NpgsqlConnection("server = 127.0.0.1; Database = postgres; user id = postgres; password = pg123;");
        }

        private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        // access url is; http://localhost:57744/country?countryName=Aperi
        [HttpPost]
        public object Add(string countryName)
        {
            using (var connection = GetDbConnection())
            {
                try
                {
                    var cntr = new Country { CountryName = countryName, LastUpdate = DateTime.Now };
                    connection.OpenIfNot();
                    var result = connection.InsertAndGetId(cntr);
                    return result;
                }
                finally
                { connection.CloseIfNot(); }
            }
        }

        // access url is; https://localhost:44311/api/country/get?countryId=1
        // 20210728135107
        // http://localhost:57744/country?countryId=15
        [HttpGet]
        public Country Get(long? countryId)
        {
            Country country = new Country();

            using (var connection = GetDbConnection())
            {
                try
                {
                    connection.OpenIfNot();
                    //country = connection.GetById<Country>(commandTimeout: null, countryId ?? 0L);
                    country = connection.FirstOrDefault<Country>(q => q.CountryId == countryId);
                }
                finally
                { connection.CloseIfNot(); }
            }
            return country;
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

        [Column("last_update", Order = 3)]
        public DateTime LastUpdate
        { get; set; }
    }
}