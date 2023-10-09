using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class PasswordModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "User",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "Passwordkey",
                table: "User",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passwordkey",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
