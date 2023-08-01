using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SKILLS",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKILLS", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bithdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PROJECTS",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    freelancer_id = table.Column<int>(type: "int", nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    started_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    finished_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS", x => x.id);
                    table.ForeignKey(
                        name: "FK_PROJECTS_USERS_client_id",
                        column: x => x.client_id,
                        principalTable: "USERS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PROJECTS_USERS_freelancer_id",
                        column: x => x.freelancer_id,
                        principalTable: "USERS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USERS_SKILLS",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    skill_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_SKILLS", x => new { x.user_id, x.skill_id });
                    table.ForeignKey(
                        name: "FK_USERS_SKILLS_SKILLS_skill_id",
                        column: x => x.skill_id,
                        principalTable: "SKILLS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USERS_SKILLS_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PROJECTS_COMMENTS",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS_COMMENTS", x => x.id);
                    table.ForeignKey(
                        name: "FK_PROJECTS_COMMENTS_PROJECTS_project_id",
                        column: x => x.project_id,
                        principalTable: "PROJECTS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECTS_COMMENTS_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJECTS_client_id",
                table: "PROJECTS",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECTS_freelancer_id",
                table: "PROJECTS",
                column: "freelancer_id");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECTS_COMMENTS_project_id",
                table: "PROJECTS_COMMENTS",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECTS_COMMENTS_user_id",
                table: "PROJECTS_COMMENTS",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_SKILLS_skill_id",
                table: "USERS_SKILLS",
                column: "skill_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROJECTS_COMMENTS");

            migrationBuilder.DropTable(
                name: "USERS_SKILLS");

            migrationBuilder.DropTable(
                name: "PROJECTS");

            migrationBuilder.DropTable(
                name: "SKILLS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
