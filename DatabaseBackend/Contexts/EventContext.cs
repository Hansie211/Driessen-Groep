using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace DatabaseBackend {
    public class EventContext : DbContext {
        public EventContext( [NotNullAttribute] DbContextOptions options ) : base( options ) {

            this.Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventProgram> EventPrograms { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {


            modelBuilder.Entity<Event>().HasData(

                new Event() { ID = 10, Name = "EventName", Date = DateTime.Now }
                
            );
        }
    }
}
