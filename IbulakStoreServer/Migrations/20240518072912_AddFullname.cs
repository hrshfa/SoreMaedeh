using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class AddFullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9de56b3f-69f1-4fac-ad77-56789a422faa");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Baskets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Baskets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab4b865d-8020-47e7-b12d-776c43e84360", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash" },
                values: new object[] { "f50d6839-02b7-4d79-99ba-2cf0d4362d79", "حمیدرضا شهشهانی", "AQAAAAIAAYagAAAAEPwJHgfaLbeJiwVcvm8YQsgSDkMRHyvbp7iO4yVVyYURsuU4nIPKKe4ryo8BovCLpA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId1",
                table: "Baskets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId1",
                table: "Baskets",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId1",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserId1",
                table: "Baskets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab4b865d-8020-47e7-b12d-776c43e84360");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Baskets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9de56b3f-69f1-4fac-ad77-56789a422faa", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "567f399d-3463-4ec8-9ae4-41dc0c606dd0", "AQAAAAIAAYagAAAAEOp8qwungFjlKfBzlwX1efUfu7ovI29vWt18CXWz4BgICFdUqLp4kIr6+3JyBpYtpg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
