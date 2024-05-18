using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9de56b3f-69f1-4fac-ad77-56789a422faa", null, "User", "USER" },
                    { "a2a2df88-2952-408d-9c34-eca9177d92ac", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2426167f-842e-4933-ae72-d8dfe34abf78", 0, "567f399d-3463-4ec8-9ae4-41dc0c606dd0", "hr.shahshahani@gmail.com", true, false, null, "hr.shahshahani@gmail.com", "09119660028", "AQAAAAIAAYagAAAAEOp8qwungFjlKfBzlwX1efUfu7ovI29vWt18CXWz4BgICFdUqLp4kIr6+3JyBpYtpg==", null, true, "", false, "09119660028" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a2a2df88-2952-408d-9c34-eca9177d92ac", "2426167f-842e-4933-ae72-d8dfe34abf78" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9de56b3f-69f1-4fac-ad77-56789a422faa");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2a2df88-2952-408d-9c34-eca9177d92ac", "2426167f-842e-4933-ae72-d8dfe34abf78" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2a2df88-2952-408d-9c34-eca9177d92ac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78");
        }
    }
}
