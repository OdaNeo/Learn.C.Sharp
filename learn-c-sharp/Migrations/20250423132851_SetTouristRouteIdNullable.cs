using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class SetTouristRouteIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristRoutePictures_TouristRouts_TouristRouteId",
                table: "TouristRoutePictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristRouteId",
                table: "TouristRoutePictures",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecccef18-ab0f-4ef7-aff2-709ececd6ff6", "AQAAAAIAAYagAAAAEGZ3U4Ndow0nOPAqXVUTyMokGtH9Mr/mf2WoiGfMWQ8x7cqE3Ci9n5FAS1XK0CE31Q==", "caac1c7f-f606-4c1f-b3f1-fce428baa5e5" });

            migrationBuilder.AddForeignKey(
                name: "FK_TouristRoutePictures_TouristRouts_TouristRouteId",
                table: "TouristRoutePictures",
                column: "TouristRouteId",
                principalTable: "TouristRouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristRoutePictures_TouristRouts_TouristRouteId",
                table: "TouristRoutePictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristRouteId",
                table: "TouristRoutePictures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f18cd35b-92ce-48c4-a225-ac3c70ce1a16", "AQAAAAIAAYagAAAAEHV9z22nNSpK9X01nbRX1igBrz0VH+JrZWPcodoe+4WR+Q7jJMeVmmUZmeYbA6XdMQ==", "e7768dc1-dbcc-4236-a347-03b6ab8a5f55" });

            migrationBuilder.AddForeignKey(
                name: "FK_TouristRoutePictures_TouristRouts_TouristRouteId",
                table: "TouristRoutePictures",
                column: "TouristRouteId",
                principalTable: "TouristRouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
