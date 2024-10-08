﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240917071521_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Clothes", b =>
                {
                    b.Property<string>("ClothesID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClothesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClothesID");

                    b.HasIndex("UserID");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("Domain.Entities.ClothesCategory", b =>
                {
                    b.Property<string>("CategoryID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("ClothesCategories");
                });

            modelBuilder.Entity("Domain.Entities.ClothesImage", b =>
                {
                    b.Property<string>("ImageID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClothesID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ImageID");

                    b.HasIndex("ClothesID");

                    b.ToTable("ClothesImages");
                });

            modelBuilder.Entity("Domain.Entities.ClothesToCategory", b =>
                {
                    b.Property<string>("ClothesID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClothesID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("ClothesToCategories");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<string>("PaymentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethodID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PaymentID");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("UserID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<string>("PaymentMethodID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<string>("RoleID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.UserAccount", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Domain.Entities.UserProfile", b =>
                {
                    b.Property<string>("ProfileID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProfileID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Domain.Entities.Clothes", b =>
                {
                    b.HasOne("Domain.Entities.UserAccount", "UserAccount")
                        .WithMany("Clothes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.Entities.ClothesImage", b =>
                {
                    b.HasOne("Domain.Entities.Clothes", "Clothes")
                        .WithMany("ClothesImages")
                        .HasForeignKey("ClothesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothes");
                });

            modelBuilder.Entity("Domain.Entities.ClothesToCategory", b =>
                {
                    b.HasOne("Domain.Entities.ClothesCategory", "ClothesCategory")
                        .WithMany("ClothesToCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Clothes", "Clothes")
                        .WithMany("ClothesToCategories")
                        .HasForeignKey("ClothesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothes");

                    b.Navigation("ClothesCategory");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserAccount", "UserAccount")
                        .WithMany("Payments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.Entities.UserAccount", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UserAccounts")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.UserProfile", b =>
                {
                    b.HasOne("Domain.Entities.UserAccount", "UserAccount")
                        .WithOne("UserProfile")
                        .HasForeignKey("Domain.Entities.UserProfile", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.Entities.Clothes", b =>
                {
                    b.Navigation("ClothesImages");

                    b.Navigation("ClothesToCategories");
                });

            modelBuilder.Entity("Domain.Entities.ClothesCategory", b =>
                {
                    b.Navigation("ClothesToCategories");
                });

            modelBuilder.Entity("Domain.Entities.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UserAccounts");
                });

            modelBuilder.Entity("Domain.Entities.UserAccount", b =>
                {
                    b.Navigation("Clothes");

                    b.Navigation("Payments");

                    b.Navigation("UserProfile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
