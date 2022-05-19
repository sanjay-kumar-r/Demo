using EntityFrameworkDemo.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.EmpUtils
{
    public class TempItemsDBContext : DbContext
    {
        public TempItemsDBContext(DbContextOptions<TempItemsDBContext> options) : base(options)
        {
        }

        public DbSet<TempItems> TempItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TempItems>()
                .HasKey(d => d.Id)
                .HasName("PrimaryKey_TempId");
            //PopulateDepartments(ref modelBuilder);
        }
    }
}
