using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeployGame.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<LinkToDownloadGame> LinkToDownloadGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.MaxTry).HasDefaultValue(3);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Key>(entity =>
            {
                entity.ToTable("keys");
                entity.HasKey(e => e.KeyValue);
                entity.Property(e => e.KeyValue).IsRequired();
                entity.Property(e => e.IsUsed).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
                entity
                    .HasOne(e => e.User)
                    .WithMany(u => u.Keys)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LinkToDownloadGame>(entity =>
            {
                entity.ToTable("link_to_download_game");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Link).IsRequired().HasMaxLength(450);
                entity.Property(e => e.Avalible).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
