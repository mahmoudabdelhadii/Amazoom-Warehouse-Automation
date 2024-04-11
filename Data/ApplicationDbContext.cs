using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Amazoom.Models;

namespace Amazoom.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Item> Item { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Truck> Truck { get; set; }
        
    }
}
