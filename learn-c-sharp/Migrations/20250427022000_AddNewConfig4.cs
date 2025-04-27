using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learn_c_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConfig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fe473d0-62c9-4ae0-9df5-9455eb8f3b42", "AQAAAAIAAYagAAAAEMV2qk6b+62iCfXiDNFnKk51rJvV9KB1zSvGWyl2VJEA1jto+dbGsGjM+KNt3cMBMg==", "269817a7-d587-4886-aff7-4bff768c393f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72289d37-b45f-4d28-8af0-a128c470f281", "AQAAAAIAAYagAAAAEKOGpfnru2cUUuBtJzszDjE0J+PDnG2mPPALK+oRMgiwKFSiP3+RFumnlt+Gl2ljkQ==", "b734a978-d3c2-4c01-b16e-20bacde6df00" });
        }
    }
}
