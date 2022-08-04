﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Models.Authorization;
using Portfolio.Models.Contact;

namespace Portfolio.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DatabaseContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                Role.GetDefaultUser(),
                Role.GetDefaultAdmin());
        }

        public DbSet<ContactModel> Contact { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permision> Permisions { get; set; } = null!;
    }
}