﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineMobileRecharge.Data;

#nullable disable

namespace OnlineMobileRecharge.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240620164946_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.CallerTune", b =>
                {
                    b.Property<int>("Tune_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Tune_Id"));

                    b.Property<string>("Tune_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tune_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tune_Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tune_Price")
                        .HasColumnType("float");

                    b.HasKey("Tune_Id");

                    b.ToTable("CallerTunes");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Contact", b =>
                {
                    b.Property<int>("Contact_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Contact_Id"));

                    b.Property<string>("Contact_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact_Intrest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact_Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_Added")
                        .HasColumnType("datetime2");

                    b.HasKey("Contact_Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.CustomRechargeTransaction", b =>
                {
                    b.Property<int>("CustomRecharge_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomRecharge_Id"));

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Mobile_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Recharge_Amount")
                        .HasColumnType("float");

                    b.Property<double>("Recharge_Price")
                        .HasColumnType("float");

                    b.Property<string>("Session_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxRateTax_Id")
                        .HasColumnType("int");

                    b.Property<int>("Tax_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Transaction_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomRecharge_Id");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("TaxRateTax_Id");

                    b.ToTable("CustomRechargeTransactions");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Feedback", b =>
                {
                    b.Property<int>("Feedback_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Feedback_Id"));

                    b.Property<DateTime>("Date_Added")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Feedback_Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Feedback_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Feedback_Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Newsletter", b =>
                {
                    b.Property<int>("Newsletter_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Newsletter_Id"));

                    b.Property<DateTime>("Date_Added")
                        .HasColumnType("datetime2");

                    b.Property<string>("Newsletter_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Newsletter_Id");

                    b.ToTable("Newsletter");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Package", b =>
                {
                    b.Property<int>("Package_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Package_Id"));

                    b.Property<long>("Package_Data")
                        .HasColumnType("bigint");

                    b.Property<string>("Package_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Package_Duration")
                        .HasColumnType("int");

                    b.Property<string>("Package_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Package_Off_Net_Mins")
                        .HasColumnType("bigint");

                    b.Property<long>("Package_On_Net_Mins")
                        .HasColumnType("bigint");

                    b.Property<double>("Package_Price")
                        .HasColumnType("float");

                    b.Property<long>("Package_SMS")
                        .HasColumnType("bigint");

                    b.Property<int>("Package_Type")
                        .HasColumnType("int");

                    b.HasKey("Package_Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.PackageTransaction", b =>
                {
                    b.Property<int>("PackageTransaction_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageTransaction_Id"));

                    b.Property<string>("Mobile_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Package_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Payment_Completed")
                        .HasColumnType("bit");

                    b.Property<string>("Session_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Transaction_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PackageTransaction_Id");

                    b.HasIndex("Package_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("PackageTransactions");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Recharge", b =>
                {
                    b.Property<int>("Recharge_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Recharge_Id"));

                    b.Property<double>("Recharge_Amount")
                        .HasColumnType("float");

                    b.Property<string>("Recharge_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recharge_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Recharge_Price")
                        .HasColumnType("float");

                    b.Property<double>("Recharge_Tax_Rate")
                        .HasColumnType("float");

                    b.Property<double>("Recharge_Taxed_Amount")
                        .HasColumnType("float");

                    b.Property<int>("Recharge_Type")
                        .HasColumnType("int");

                    b.HasKey("Recharge_Id");

                    b.ToTable("Recharges");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.RechargeTransaction", b =>
                {
                    b.Property<int>("RechargeTransaction_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RechargeTransaction_Id"));

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Mobile_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Payment_Completed")
                        .HasColumnType("bit");

                    b.Property<int>("Recharge_Id")
                        .HasColumnType("int");

                    b.Property<string>("Session_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Transaction_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RechargeTransaction_Id");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("Recharge_Id");

                    b.ToTable("RechargeTransactions");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Service", b =>
                {
                    b.Property<int>("Service_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Service_Id"));

                    b.Property<int>("Caller_Tune_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Do_Not_Disturb")
                        .HasColumnType("bit");

                    b.Property<string>("User_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Service_Id");

                    b.HasIndex("Caller_Tune_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.ServiceTransaction", b =>
                {
                    b.Property<int>("ServiceTransaction_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceTransaction_Id"));

                    b.Property<string>("Mobile_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Session_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Transaction_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tune_Id")
                        .HasColumnType("int");

                    b.Property<string>("User_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ServiceTransaction_Id");

                    b.HasIndex("Tune_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("ServiceTransactions");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.TaxRate", b =>
                {
                    b.Property<int>("Tax_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Tax_Id"));

                    b.Property<string>("Tax_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tax_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tax_Rate")
                        .HasColumnType("float");

                    b.HasKey("Tax_Id");

                    b.ToTable("TaxRates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.CustomRechargeTransaction", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.HasOne("OnlineMobileRecharge.Models.TaxRate", "TaxRate")
                        .WithMany()
                        .HasForeignKey("TaxRateTax_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");

                    b.Navigation("TaxRate");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.PackageTransaction", b =>
                {
                    b.HasOne("OnlineMobileRecharge.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("Package_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.RechargeTransaction", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.HasOne("OnlineMobileRecharge.Models.Recharge", "Recharge")
                        .WithMany()
                        .HasForeignKey("Recharge_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");

                    b.Navigation("Recharge");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.Service", b =>
                {
                    b.HasOne("OnlineMobileRecharge.Models.CallerTune", "Caller_Tune")
                        .WithMany()
                        .HasForeignKey("Caller_Tune_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Caller_Tune");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("OnlineMobileRecharge.Models.ServiceTransaction", b =>
                {
                    b.HasOne("OnlineMobileRecharge.Models.CallerTune", "CallerTune")
                        .WithMany()
                        .HasForeignKey("Tune_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CallerTune");

                    b.Navigation("IdentityUser");
                });
#pragma warning restore 612, 618
        }
    }
}
