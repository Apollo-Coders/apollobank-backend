using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApolloBank.Migrations
{
    /// <inheritdoc />
    public partial class AccountIdRefence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Accounts_Account_Id",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Account_Id",
                table: "CreditCard");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Transactions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "CreditCards",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_Account_Id",
                table: "CreditCards",
                newName: "IX_CreditCards_AccountId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlocked",
                table: "CreditCard",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Accounts_AccountId",
                table: "CreditCards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Accounts_AccountId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "Account_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_Account_Id");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "CreditCards",
                newName: "Account_Id");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_AccountId",
                table: "CreditCards",
                newName: "IX_CreditCards_Account_Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlocked",
                table: "CreditCard",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Account_Id",
                table: "CreditCard",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Accounts_Account_Id",
                table: "CreditCards",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
