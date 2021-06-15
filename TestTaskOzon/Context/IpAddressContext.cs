using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskOzon
{
    public class IpAddressContext : DbContext
    {
        public DbSet<IpAddressDiapazon> IpAddressDiapazon { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IpAddressDiapazon>().HasKey(u => new { u.From, u.To });
        }
        public IpAddressContext(DbContextOptions<IpAddressContext> options)
            : base(options)
       {
            Database.EnsureCreated();
        }
    }
}
