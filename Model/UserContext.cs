﻿using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Model.Persistence.Configuration;

namespace Model;

public class UserContext : DbContext {
    public DbSet<User?> User { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options) {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new UserConfig());
        base.OnModelCreating(modelBuilder);
    }
}