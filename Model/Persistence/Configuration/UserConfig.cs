﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Domain;

namespace Model.Persistence.Configuration;

public class UserConfig : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Lastname).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Surname).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(200).IsRequired();
        builder.Property(e => e.PasswordHash).HasColumnType("bytea").IsRequired();
        builder.Property(e => e.PasswordSalt).HasColumnType("bytea").IsRequired();
        builder.Property(e => e.Guid).IsRequired();
    }
}