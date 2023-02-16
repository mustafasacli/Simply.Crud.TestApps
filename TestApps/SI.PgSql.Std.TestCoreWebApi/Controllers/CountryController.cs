using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simply.Crud;
using Simply.Crud.DatabaseExtensions;
using Simply.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SI.PgSql.Std.TestCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ISimpleDatabase _database;

        public CountryController(ILogger<CountryController> logger, ISimpleDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        // access url is; http://localhost:57744/country?countryName=Aperi
        [HttpPost]
        public object Add(string countryName)
        {
            var cntr = new Country { CountryName = countryName, LastUpdate = DateTime.Now };
            var result = _database.InsertAndGetId(cntr);
            return result;
        }

        // access url is; https://localhost:44311/api/country/get?countryId=1
        // 20210728135107
        // http://localhost:57744/country?countryId=15
        [HttpGet]
        public Country Get(long? countryId)
        {
            Country country = _database.FirstOrDefault<Country>(q => q.CountryId == countryId);
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