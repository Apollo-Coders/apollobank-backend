using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApolloBank.Migrations
{
    /// <inheritdoc />
    public partial class TransactionStatusChecker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionStatusChecker",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Accounts",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionStatusChecker",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "Balance",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
