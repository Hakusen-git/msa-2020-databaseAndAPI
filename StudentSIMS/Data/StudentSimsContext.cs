using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSIMS.Data
{
    public class StudentSimsContext : DbContext
    {
        public StudentSimsContext() { }

        public StudentSimsContext(DbContextOptions<StudentSimsContext> options) : base(options) 
        {
           
        }

        public DbSet<Address> Address { get; set; }

        public DbSet<Student> Student { get; set; }

        
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();


            optionsBuilder.UseSqlServer(configuration.GetConnectionString("schoolSIMSConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.addresses)
                .WithOne(s => s.student)
                .HasForeignKey(s => s.studentID)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
