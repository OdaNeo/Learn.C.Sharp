using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b0f8fe2-611c-48ee-b443-4a2855a30573", "AQAAAAIAAYagAAAAECSRHNChe6SMJ2fwxXS4GrtsQUSAQuPY1hcvdDZqFB3ze5yb0BKj9JcQqsKSUwXuoQ==", "5ac49f6c-f871-4bea-92c1-6e4f5b0a9374" });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("0e09d08e-0ad4-42c0-a0f2-d1c9e3c99a28"), "90184155-dee0-40c9-bb1e-b5ed07afc04e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("0e09d08e-0ad4-42c0-a0f2-d1c9e3c99a28"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fe473d0-62c9-4ae0-9df5-9455eb8f3b42", "AQAAAAIAAYagAAAAEMV2qk6b+62iCfXiDNFnKk51rJvV9KB1zSvGWyl2VJEA1jto+dbGsGjM+KNt3cMBMg==", "269817a7-d587-4886-aff7-4bff768c393f" });
        }
    }
}
