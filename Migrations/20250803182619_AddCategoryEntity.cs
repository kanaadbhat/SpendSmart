using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceApp.Migrations
{
    /// <inheritdoc />
   public partial class AddCategoryEntity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Drop old column 'Category' from 'Expenses' table
        migrationBuilder.DropColumn(
            name: "Category",
            table: "Expenses");

        // Alter 'Description' column to enforce max length
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Expenses",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        // Alter 'Date' column to add default value
        migrationBuilder.AlterColumn<DateTime>(
            name: "Date",
            table: "Expenses",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "GETDATE()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        // First, add the new 'CategoryId' column as nullable (temporarily to avoid non-null issue)
        migrationBuilder.AddColumn<int>(
            name: "CategoryId",
            table: "Expenses",
            type: "int",
            nullable: true);  // Make it nullable to avoid issues with existing rows

        // Update existing rows with a valid 'CategoryId' (e.g., 'Other' category with Id 8)
        migrationBuilder.Sql("UPDATE Expenses SET CategoryId = 8 WHERE CategoryId IS NULL");

        // After updating, alter 'CategoryId' column to be NOT NULL
        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Expenses",
            type: "int",
            nullable: false);

        // Create 'Categories' table
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        // Insert initial categories into 'Categories' table
        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "Color", "CreatedAt", "Description", "Name" },
            values: new object[,]
            {
                { 1, "#EF4444", DateTime.UtcNow, "Restaurants, groceries, and dining out", "Food & Dining" },
                { 2, "#3B82F6", DateTime.UtcNow, "Gas, public transport, and vehicle expenses", "Transportation" },
                { 3, "#10B981", DateTime.UtcNow, "Clothing, electronics, and general shopping", "Shopping" },
                { 4, "#F59E0B", DateTime.UtcNow, "Movies, games, and leisure activities", "Entertainment" },
                { 5, "#8B5CF6", DateTime.UtcNow, "Electricity, water, internet, and phone bills", "Bills & Utilities" },
                { 6, "#EC4899", DateTime.UtcNow, "Medical expenses and health-related costs", "Healthcare" },
                { 7, "#06B6D4", DateTime.UtcNow, "Books, courses, and educational expenses", "Education" },
                { 8, "#6B7280", DateTime.UtcNow, "Miscellaneous expenses", "Other" }
            });

        // Create index on 'CategoryId' in 'Expenses' table
        migrationBuilder.CreateIndex(
            name: "IX_Expenses_CategoryId",
            table: "Expenses",
            column: "CategoryId");

        // Add foreign key constraint to link 'Expenses' and 'Categories'
        migrationBuilder.AddForeignKey(
            name: "FK_Expenses_Categories_CategoryId",
            table: "Expenses",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Reverse the foreign key, table, and index changes
        migrationBuilder.DropForeignKey(
            name: "FK_Expenses_Categories_CategoryId",
            table: "Expenses");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropIndex(
            name: "IX_Expenses_CategoryId",
            table: "Expenses");

        migrationBuilder.DropColumn(
            name: "CategoryId",
            table: "Expenses");

        // Revert 'Description' column back to the previous type
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Expenses",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);

        // Revert 'Date' column back to the previous state (no default value)
        migrationBuilder.AlterColumn<DateTime>(
            name: "Date",
            table: "Expenses",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "GETDATE()");

        // Revert 'Amount' column to old type and allow NULLs
        migrationBuilder.AddColumn<double>(
            name: "AmountOld",
            table: "Expenses",
            type: "float",
            nullable: true);

        migrationBuilder.Sql("UPDATE Expenses SET AmountOld = CAST(Amount AS float)");

        migrationBuilder.DropColumn(
            name: "Amount",
            table: "Expenses");

        migrationBuilder.RenameColumn(
            name: "AmountOld",
            table: "Expenses",
            newName: "Amount");

        migrationBuilder.AlterColumn<double>(
            name: "Amount",
            table: "Expenses",
            type: "float",
            nullable: false);

        migrationBuilder.AddColumn<string>(
            name: "Category",
            table: "Expenses",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}

}
