using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.C.Sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91728e5b-e005-431c-8cf2-ad358ea9eb79", "AQAAAAIAAYagAAAAEMzFERZeM1N58iykTGphRDIEKGYe0DKdohPLsc8c5iqOGeqbGCJzMvepz82G/bIBIw==", "bd93e0b9-c38d-43a3-8183-e9eb35c5cda8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecccef18-ab0f-4ef7-aff2-709ececd6ff6", "AQAAAAIAAYagAAAAEGZ3U4Ndow0nOPAqXVUTyMokGtH9Mr/mf2WoiGfMWQ8x7cqE3Ci9n5FAS1XK0CE31Q==", "caac1c7f-f606-4c1f-b3f1-fce428baa5e5" });
        }
    }
}
