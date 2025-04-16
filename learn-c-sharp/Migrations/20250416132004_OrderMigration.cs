using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "LineItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreateDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionMetadata = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0457d789-a87d-4ee7-a3c8-ddabfa96d993", "AQAAAAIAAYagAAAAEDz9LI4m8i43NjUo87frCz6tjPTW99q1IIpJBhKyXrswhqwtt93s03jdZJl6iEITpQ==", "501bfc4d-91c7-4a4f-844d-6e85c45ed6c1" });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aae31ac-f07c-414b-8293-d600b39fcbcc", "AQAAAAIAAYagAAAAEPmzHqd7mXDtxGQJJbeCPPsJ4l6VtIFreaDfLZ/NHPJ8wiNmBdXfYoIP6+tW8uzqSA==", "d4ce462b-e39f-4794-be7b-4f249a9840bc" });
        }
    }
}
