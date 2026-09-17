using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedUser_CredentialsForAuthenticationPurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Credentials",
                columns: table => new
                {
                    User_Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Credentials", x => x.User_Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Credentials");
        }
    }
}
