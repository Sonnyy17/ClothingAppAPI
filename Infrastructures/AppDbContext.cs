using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesImage> ClothImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothesToCategory> ClothToCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.UserAccount)
                .WithMany()
                .HasForeignKey(up => up.UserID);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.UserAccount)
                .WithMany()
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey(p => p.PaymentMethodID);

            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.UserAccount)
                .WithMany()
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<ClothesImage>()
                .HasOne(ci => ci.Clothes)
                .WithMany()
                .HasForeignKey(ci => ci.ClothesID);

            modelBuilder.Entity<ClothesToCategory>()
                .HasOne(ctc => ctc.Clothes)
                .WithMany()
                .HasForeignKey(ctc => ctc.ClothesID);

            modelBuilder.Entity<ClothesToCategory>()
                .HasOne(ctc => ctc.Category)
                .WithMany()
                .HasForeignKey(ctc => ctc.CategoryID);
        }
    }
}
