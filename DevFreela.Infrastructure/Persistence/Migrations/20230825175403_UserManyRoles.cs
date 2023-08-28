using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserManyRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "USERS");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "USERS",
                newName: "password");

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "USERS_ROLES",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_ROLES", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_ROLES_role_id",
                        column: x => x.role_id,
                        principalTable: "ROLES",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_ROLES_role_id",
                table: "USERS_ROLES",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS_ROLES");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "USERS",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
