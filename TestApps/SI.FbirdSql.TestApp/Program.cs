using FirebirdSql.Data.FirebirdClient;
using Simply.Crud.DatabaseExtensions;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SI.FbirdSql.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connectionString = @"User=SYSDBA;Password=123456;Database=C:\Depo\dev_progs\sql\db_3_0\examples.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType = 0;";

            using (ISimpleDatabase database = new SimpleDatabase(
                SimpleDatabase.Create<FbConnection>(connectionString)))
            { /*
            var usr = new users
            {
                name = "Mustafa Saçlı",
                email = "mstscl@gmail.com",
                password = "pwd",
                remember_token = "rmbr"
            };
            usr.created_at = DateTime.Now;
            usr.updated_at = DateTime.Now;
            */
                var invoice = new Invoice
                {
                    CustomerId = 50,
                    InvoiceDate = DateTime.Now,
                    Paid = 1,
                    TotalSale = 100.65M
                };
                var result = database.InsertAndGetId(invoice);
                Console.WriteLine(result);
                database.Close();
            }

            Console.ReadKey();
        }
    }

    internal class users
    {
        [Key]
        public int id
        { get; set; }

        public string name
        { get; set; }

        public string email
        { get; set; }

        public string password
        { get; set; }

        public string remember_token
        { get; set; }

        public DateTime created_at
        { get; set; }

        public DateTime updated_at
        { get; set; }
    }

    [Table("INVOICE")]
    internal class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("INVOICE_ID")]
        public long InvoiceId
        { get; set; }

        [Column("CUSTOMER_ID")]
        public long CustomerId
        { get; set; }

        [Column("INVOICE_DATE")]
        public DateTime? InvoiceDate
        { get; set; }

        [Column("TOTAL_SALE")]
        public decimal TotalSale
        { get; set; }

        [Column("PAID")]
        public short Paid
        { get; set; } = 0;
    }
}