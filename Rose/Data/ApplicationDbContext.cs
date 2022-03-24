using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rose.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Rose.Models;

namespace Rose.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Rose.Models.ClientBindingAllViewModel> ClientBindingAllViewModel { get; set; }
    }
}
