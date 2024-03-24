using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApolloBank.Migrations
{
    /// <inheritdoc />
    public partial class transactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transaction_Type",
                table: "Transactions",
                newName: "TransactionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "Transaction_Type");
        }
    }
}
