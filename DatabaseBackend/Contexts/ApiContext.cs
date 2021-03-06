﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Models;

namespace DatabaseBackend {
    public class ApiContext : DbContext {

        public ApiContext() : base() {

        }

        public ApiContext( [NotNullAttribute] DbContextOptions options ) : base( options ) {

            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventOwnership> EventOwnerships { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<EventProgram> EventPrograms { get; set; }

        public DbSet<EventReview> EventReviews { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-3.1
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#many-to-many

            modelBuilder.Entity<User>().HasKey( u => new { u.ID } );
            modelBuilder.Entity<User>().HasAlternateKey( u => new { u.Email } ); // Add 'Email' as alternate primary key
            modelBuilder.Entity<User>().HasMany( u => u.Ownerships ).WithOne( o => o.User );

            modelBuilder.Entity<Event>().HasKey( e => new { e.ID } );
            modelBuilder.Entity<Event>().HasMany( e => e.Programs ).WithOne( p => p.Event );
            modelBuilder.Entity<Event>().HasMany( e => e.Ownerships ).WithOne( o => o.Event );
            modelBuilder.Entity<Event>().HasMany( e => e.Speakers ).WithOne( s => s.Event );
            modelBuilder.Entity<Event>().HasMany( e => e.Reviews ).WithOne( r => r.Event );

            modelBuilder.Entity<Event>().HasData(

                new Event() { ID = 10, Title = "EventName", Date = DateTime.Now, Description = "beschrijving", Location = "hier", }
            );


            modelBuilder.Entity<User>().HasData(

                new User() { ID = 11, FirstName = "First", LastName = "LastName", Email = "EMAIL", PasswordHash = "", PasswordSalt = "", SecurityLevel = SecurityLevel.User }
            );
        }
    }
}
