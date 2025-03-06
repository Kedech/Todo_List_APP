using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_List_API.Migrations
{
    /// <inheritdoc />
    public partial class AddSaltUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Salt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "Password");
        }
    }
}
