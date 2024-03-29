﻿// <auto-generated />
using System;
using ApolloBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApolloBank.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240315010143_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Balance")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CreditLimit")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

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
                        .HasColumnType("TEXT");

                    b.Property<string>("Complement")
                        .HasColumnType("TEXT");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Policies")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Address");
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

                    b.Property<float>("TotalAlocatedCredit")
                        .HasColumnType("REAL");

                    b.Property<float>("TotalCreditLimit")
                        .HasColumnType("REAL");

                    b.Property<float>("TotalCreditUsed")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("DDD")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Users");
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

                    b.HasOne("ApolloBank.Models.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("ApolloBank.Models.User", "AddressId");

                    b.Navigation("Account");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ApolloBank.Models.Account", b =>
                {
                    b.Navigation("CreditCard");

                    b.Navigation("CreditCards");

                    b.Navigation("Transactions");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("ApolloBank.Models.Address", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
