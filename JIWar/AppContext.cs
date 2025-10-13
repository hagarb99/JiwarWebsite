using GEWAR.Models;
using GEWAR.Models.BookingRating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR
{

    /// <summary>
    /// SOMEEE SQL Server connection string
    /// /user id=hagarb_SQLLogin_1;pwd=zlvwboiwro
    /// </summary>
    public class AppContext : DbContext
    {
        //tables
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingRating> BookingRating { get; set; }
        public DbSet<Chat> Chats { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=JIWARDB.mssql.somee.com;packet size=4096;user id=hagarb_SQLLogin_1;pwd=zlvwboiwro;data source=JIWARDB.mssql.somee.com;persist security info=False;initial catalog=JIWARDB;TrustServerCertificate=True");
        }

    }
}
