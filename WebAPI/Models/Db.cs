using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class Db : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=messageverwaltung_db;user=root;password=");
        }
    }
}
