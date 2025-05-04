using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.C.Sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddEditorRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dacc15f0-bffd-477c-aa59-27b40b93b14e", null, "Editor", "EDITOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aae31ac-f07c-414b-8293-d600b39fcbcc", "AQAAAAIAAYagAAAAEPmzHqd7mXDtxGQJJbeCPPsJ4l6VtIFreaDfLZ/NHPJ8wiNmBdXfYoIP6+tW8uzqSA==", "d4ce462b-e39f-4794-be7b-4f249a9840bc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dacc15f0-bffd-477c-aa59-27b40b93b14e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d77cee4b-bbfe-4d47-af1d-34cdcdfc80a3", "AQAAAAIAAYagAAAAECVjwKcmIRumj0dRzSoXvLcTZDPbTp3ExlvxcP4LyM79KWGd5mcEzagSVsQk6+a75Q==", "df0dce1c-16e6-4bf3-b0f6-9e2ad9a5d4e4" });
        }
    }
}
