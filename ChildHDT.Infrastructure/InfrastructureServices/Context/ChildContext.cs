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
        // ATTRIBUTES
        public DbSet<Child> Children { get; set; }

        public string connectionString;

        public ChildContext (string connectionString)
        {
            this.connectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roleConverter = new ValueConverter<Role, string>(
                v => RoleConverterHelper.SerializeRole(v),
                v => RoleConverterHelper.DeserializeRole(v)
            );

            modelBuilder.Entity<Child>()
                .Property(c => c.Role)
                .HasConversion(roleConverter);
        }
    }
}
