using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        // Khởi tạo DbContext với cấu hình từ bên ngoài
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Định nghĩa các DbSet đại diện cho các bảng trong cơ sở dữ liệu
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<ClothesCategory> ClothesCategories { get; set; }
        public DbSet<ClothesToCategory> ClothesToCategories { get; set; }


        // Cấu hình các quan hệ giữa các bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập khóa chính
            modelBuilder.Entity<UserAccount>().HasKey(u => u.UserID);
            modelBuilder.Entity<UserProfile>().HasKey(p => p.ProfileID);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleID);
            modelBuilder.Entity<Payment>().HasKey(p => p.PaymentID);
            modelBuilder.Entity<PaymentMethod>().HasKey(pm => pm.PaymentMethodID);
            modelBuilder.Entity<Clothes>().HasKey(c => c.ClothesID);
            modelBuilder.Entity<ClothesImage>().HasKey(ci => ci.ImageID);
            modelBuilder.Entity<ClothesCategory>().HasKey(cc => cc.CategoryID);

            // Thiết lập khóa chính cho bảng quan hệ nhiều-nhiều
            modelBuilder.Entity<ClothesToCategory>()
                .HasKey(ctc => new { ctc.ClothesID, ctc.CategoryID });

            // Thiết lập quan hệ giữa UserAccount và UserProfile (1-1)
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.UserAccount)
                .HasForeignKey<UserProfile>(p => p.UserID);

            // Thiết lập quan hệ giữa UserAccount và Role (1-n)
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.Role)
                .WithMany(r => r.UserAccounts)
                .HasForeignKey(u => u.RoleID);

            // Thiết lập quan hệ giữa Payment và UserAccount (n-1)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.UserAccount)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserID);

            // Thiết lập quan hệ giữa Payment và PaymentMethod (n-1)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany(pm => pm.Payments)
                .HasForeignKey(p => p.PaymentMethodID);

            // Thiết lập quan hệ giữa Clothes và UserAccount (n-1)
            modelBuilder.Entity<Clothes>()
                .HasOne(c => c.UserAccount)
                .WithMany(u => u.Clothes)
                .HasForeignKey(c => c.UserID);

            // Thiết lập quan hệ giữa ClothesImage và Clothes (n-1)
            modelBuilder.Entity<ClothesImage>()
                .HasOne(ci => ci.Clothes)
                .WithMany(c => c.ClothesImages)
                .HasForeignKey(ci => ci.ClothesID);

            // Thiết lập quan hệ nhiều-nhiều giữa Clothes và ClothesCategory
            modelBuilder.Entity<ClothesToCategory>()
                .HasOne(ctc => ctc.Clothes)
                .WithMany(c => c.ClothesToCategories)
                .HasForeignKey(ctc => ctc.ClothesID);

            modelBuilder.Entity<ClothesToCategory>()
                .HasOne(ctc => ctc.ClothesCategory)
                .WithMany(cc => cc.ClothesToCategories)
                .HasForeignKey(ctc => ctc.CategoryID);

            // Thiết lập cho thực thể Payment
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
        }

    }


}
