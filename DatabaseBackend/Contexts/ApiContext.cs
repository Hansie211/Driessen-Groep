using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace DatabaseBackend {
    public class ApiContext : DbContext {
        public ApiContext( [NotNullAttribute] DbContextOptions options ) : base( options ) {

            this.Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {


            modelBuilder.Entity<Event>().HasData(

                new Event() { ID = 10, Name = "EventName", Date = DateTime.Now }
            );

            modelBuilder.Entity<User>().HasAlternateKey( u => new { u.Email } );
            modelBuilder.Entity<User>().HasData(

                new User() { ID = 11, FirstName = "First", LastName = "LastName", Email = "EMAIL" }
            );

        }
    }
}
