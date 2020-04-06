using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseBackend.Contexts {
    public class UserContext : DbContext {
        public UserContext( [NotNullAttribute] DbContextOptions options ) : base( options ) {

            //this.
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {


            modelBuilder.Entity<User>().HasData(

                new Event() { ID = 10, Name = "EventName", Date = DateTime.Now }

            );
        }
    }
}
