using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApolloBank.Migrations
{
    /// <inheritdoc />
    public partial class AddingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account_Id",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Invoices",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "CreditCard",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "CreditCard",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Addresses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "status");

            migrationBuilder.AddColumn<int>(
                name: "Account_Id",
                table: "Invoices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "CreditCard",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
