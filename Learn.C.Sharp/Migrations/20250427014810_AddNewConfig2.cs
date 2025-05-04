using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.C.Sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "TouristRouts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a046c5bf-9c52-42a3-b8c9-5676148557e6", "AQAAAAIAAYagAAAAEJgXzgZ1dajRyRyujldy9PH9aBr+beJYhyQuUFz/ELgbfkzmv5kwISRkXJHZeNPTMA==", "1dbe2ed2-8360-4a36-8db3-6611ea496016" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "TouristRouts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91728e5b-e005-431c-8cf2-ad358ea9eb79", "AQAAAAIAAYagAAAAEMzFERZeM1N58iykTGphRDIEKGYe0DKdohPLsc8c5iqOGeqbGCJzMvepz82G/bIBIw==", "bd93e0b9-c38d-43a3-8183-e9eb35c5cda8" });
        }
    }
}
