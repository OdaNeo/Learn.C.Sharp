using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.C.Sharp.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42deade0-893b-4c57-abbd-29eca3731ebb", "AQAAAAIAAYagAAAAEAAGGvL1M6rRaKNEdNoZ1xPbiNlpoYnQQAZO3K8o6PpkrixBYkPhen4szjOsw5RGyw==", "48ed6a7e-c481-46cb-baca-72427d7169b1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0457d789-a87d-4ee7-a3c8-ddabfa96d993", "AQAAAAIAAYagAAAAEDz9LI4m8i43NjUo87frCz6tjPTW99q1IIpJBhKyXrswhqwtt93s03jdZJl6iEITpQ==", "501bfc4d-91c7-4a4f-844d-6e85c45ed6c1" });
        }
    }
}
