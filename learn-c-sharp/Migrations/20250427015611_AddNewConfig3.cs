using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConfig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "72289d37-b45f-4d28-8af0-a128c470f281", "AQAAAAIAAYagAAAAEKOGpfnru2cUUuBtJzszDjE0J+PDnG2mPPALK+oRMgiwKFSiP3+RFumnlt+Gl2ljkQ==", "b734a978-d3c2-4c01-b16e-20bacde6df00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
