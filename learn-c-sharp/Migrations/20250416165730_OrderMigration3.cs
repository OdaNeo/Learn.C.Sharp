using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionMetadata",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f18cd35b-92ce-48c4-a225-ac3c70ce1a16", "AQAAAAIAAYagAAAAEHV9z22nNSpK9X01nbRX1igBrz0VH+JrZWPcodoe+4WR+Q7jJMeVmmUZmeYbA6XdMQ==", "e7768dc1-dbcc-4236-a347-03b6ab8a5f55" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionMetadata",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42deade0-893b-4c57-abbd-29eca3731ebb", "AQAAAAIAAYagAAAAEAAGGvL1M6rRaKNEdNoZ1xPbiNlpoYnQQAZO3K8o6PpkrixBYkPhen4szjOsw5RGyw==", "48ed6a7e-c481-46cb-baca-72427d7169b1" });
        }
    }
}
