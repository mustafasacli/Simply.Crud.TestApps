using Simply.Crud;
using Simply.Data;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SI.FbirdSql.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FbConnection conn = new FbConnection();
            conn.ConnectionString = @"User=SYSDBA;Password=123456;Database=C:\Depo\dev_progs\sql\db_3_0\examples.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType = 0;";
            /*
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
            conn.OpenIfNot();
            var result = conn.InsertAndGetId(invoice);
            Console.WriteLine(result);
            conn.CloseIfNot();
            Console.ReadKey();
        }
    }

    class users
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    class Invoice
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
