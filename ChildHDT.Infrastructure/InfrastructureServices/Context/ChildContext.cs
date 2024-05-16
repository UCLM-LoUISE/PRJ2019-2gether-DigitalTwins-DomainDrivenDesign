﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Aquí configuras tu cadena de conexión
                optionsBuilder.UseNpgsql("Host=localhost; Database=mydatabase; Username=myuser; Password=mypassword");
            }
        }
    }
}
