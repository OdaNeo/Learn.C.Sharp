using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "7742ab0d-3cf7-492f-acbe-710645e088d3", "AQAAAAIAAYagAAAAEGsHajIKropGkB+w/4PJTIWozSxFegFn0LIRjNYrXENtXV3UTFa/kmVIPpvuCEDcBQ==", null, null, "63340fd1-e799-496d-8fa4-e599ba7bfc7c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b0f8fe2-611c-48ee-b443-4a2855a30573", "AQAAAAIAAYagAAAAECSRHNChe6SMJ2fwxXS4GrtsQUSAQuPY1hcvdDZqFB3ze5yb0BKj9JcQqsKSUwXuoQ==", "5ac49f6c-f871-4bea-92c1-6e4f5b0a9374" });
        }
    }
}
