using EntityFrameworkDemo.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.EmpUtils
{
    public class EmployeesDBContext : DbContext
    {
        public EmployeesDBContext(DbContextOptions<EmployeesDBContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
                .HasKey(d => d.Id)
                .HasName("PrimaryKey_DeptId");
            PopulateDepartments(ref modelBuilder);

            modelBuilder.Entity<Employees>()
                .HasKey(e => e.Id)
                .HasName("PrimaryKey_EmpId");
        }

        protected void PopulateDepartments(ref ModelBuilder modelBuilder)
        {
            List<Departments> departments = new List<Departments>();
            foreach (var name in Enum.GetNames(typeof(Dept)))
            {
                departments.Add(new Departments() {
                    Id = (int)Enum.Parse(typeof(Dept), name),
                    Name = name
                    //,IsDeleted = false
                });
            }
            modelBuilder.Entity<Departments>().HasData(departments);
        }
    }
}
