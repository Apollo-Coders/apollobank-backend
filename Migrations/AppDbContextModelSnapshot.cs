﻿// <auto-generated />
using System;
using ApolloBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApolloBank.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("ApolloBank.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<double>("CreditLimit")
                        .HasColumnType("REAL");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ApolloBank.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Complement")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Policies")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("ApolloBank.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Account_Id")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CreditLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("CreditUsed")
                        .HasColumnType("REAL");

                    b.Property<int>("Cvc")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("ApolloBank.Models.CreditCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Account_Id")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalAlocatedCredit")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalCreditLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalCreditUsed")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id")
                        .IsUnique();

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("ApolloBank.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Account_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("InvoicePaid")
                        .HasColumnType("REAL");

                    b.Property<double>("InvoiceTotalAmount")
                        .HasColumnType("REAL");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ApolloBank.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Account_Id")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<char>("Direction")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.Property<int>("Transaction_Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("ApolloBank.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("DDD")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ApolloBank.Models.Address", b =>
                {
                    b.HasOne("ApolloBank.Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("ApolloBank.Models.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApolloBank.Models.CreditCard", b =>
                {
                    b.HasOne("ApolloBank.Models.Account", "Account")
                        .WithMany("CreditCard")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApolloBank.Models.CreditCards", b =>
                {
                    b.HasOne("ApolloBank.Models.Account", "Account")
                        .WithOne("CreditCards")
                        .HasForeignKey("ApolloBank.Models.CreditCards", "Account_Id");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApolloBank.Models.Invoice", b =>
                {
                    b.HasOne("ApolloBank.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApolloBank.Models.Transaction", b =>
                {
                    b.HasOne("ApolloBank.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("Account_Id");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApolloBank.Models.User", b =>
                {
                    b.HasOne("ApolloBank.Models.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("ApolloBank.Models.User", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApolloBank.Models.Account", b =>
                {
                    b.Navigation("CreditCard");

                    b.Navigation("CreditCards");

                    b.Navigation("Transactions");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("ApolloBank.Models.User", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
