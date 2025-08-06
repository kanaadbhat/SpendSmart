using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDummyExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Truncate all existing expenses to avoid PK conflicts and reset identity
            migrationBuilder.Sql("TRUNCATE TABLE [Expenses]");

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description" },
                values: new object[,]
                {
                    { 1, 120.50m, 1, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Groceries" },
                    { 2, 45.00m, 2, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bus Pass" },
                    { 3, 80.00m, 3, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Shoes" },
                    { 4, 30.00m, 4, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Night" },
                    { 5, 60.00m, 5, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricity Bill" },
                    { 6, 100.00m, 6, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor Visit" },
                    { 7, 150.00m, 7, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Online Course" },
                    { 8, 40.00m, 8, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miscellaneous" },
                    { 9, 75.00m, 1, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dinner Out" },
                    { 10, 25.00m, 2, new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taxi Ride" },
                    { 11, 200.00m, 3, new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clothes Shopping" },
                    { 12, 90.00m, 4, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Concert Ticket" },
                    { 13, 35.00m, 5, new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water Bill" },
                    { 14, 60.00m, 6, new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pharmacy" },
                    { 15, 25.00m, 7, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book Purchase" },
                    { 16, 50.00m, 8, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gift" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 16);


        }
    }
}
