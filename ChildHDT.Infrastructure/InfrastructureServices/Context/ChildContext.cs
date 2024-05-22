﻿using ChildHDT.Domain.ValueObjects;
using ChildHDT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.InfrastructureServices.Context
{
    public class ChildContext : DbContext
    {
        public DbSet<Child> Children { get; set; }
        private readonly IConfiguration _configuration;

        public ChildContext(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Postgres");
                optionsBuilder.UseNpgsql(connectionString);
                //optionsBuilder.UseNpgsql("Host=localhost; Database=mydatabase; Username=myuser; Password=mypassword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roleConverter = new ValueConverter<Role, string>(
                v => v.GetType().Name,
                v => (Role)Activator.CreateInstance(Type.GetType("ChildHDT.Domain.ValueObjects." + v))
            );

            modelBuilder.Entity<Child>()
                .Property(c => c.Role)
                .HasConversion(roleConverter);
        }
    }
}
