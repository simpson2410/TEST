using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Create_DB_TaskManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Create_At = table.Column<DateTime>(nullable: false),
                    Update_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Create_At = table.Column<DateTime>(nullable: false),
                    Update_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Create_At = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Update_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ProjectCost = table.Column<double>(maxLength: 255, nullable: false),
                    TeamSize = table.Column<double>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    CurrentExpenditure = table.Column<string>(maxLength: 255, nullable: false),
                    AvailableFunds = table.Column<string>(maxLength: 255, nullable: false),
                    Start_At = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    End_At = table.Column<DateTime>(nullable: false),
                    IdClient = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Specialist = table.Column<string>(nullable: true),
                    Create_At = table.Column<DateTime>(nullable: false),
                    Update_At = table.Column<DateTime>(nullable: false),
                    IdRegion = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Regions_IdRegion",
                        column: x => x.IdRegion,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProject = table.Column<int>(maxLength: 255, nullable: false),
                    IdMember = table.Column<int>(maxLength: 255, nullable: false),
                    IdRole = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Members_IdMember",
                        column: x => x.IdMember,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Projects_IdProject",
                        column: x => x.IdProject,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_RoleMembers_IdRole",
                        column: x => x.IdRole,
                        principalTable: "RoleMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(maxLength: 255, nullable: false),
                    Create_At = table.Column<string>(nullable: true, defaultValueSql: "getdate()"),
                    Update_At = table.Column<string>(nullable: true),
                    IdProject = table.Column<int>(maxLength: 255, nullable: false),
                    IdMember = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMembers_Members_IdMember",
                        column: x => x.IdMember,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskMembers_Projects_IdProject",
                        column: x => x.IdProject,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 255, nullable: false),
                    Create_At = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IdMember = table.Column<int>(maxLength: 255, nullable: false),
                    IdTaskMember = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Members_IdMember",
                        column: x => x.IdMember,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_TaskMembers_IdTaskMember",
                        column: x => x.IdTaskMember,
                        principalTable: "TaskMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdMember",
                table: "Comments",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdTaskMember",
                table: "Comments",
                column: "IdTaskMember");

            migrationBuilder.CreateIndex(
                name: "IX_Members_IdRegion",
                table: "Members",
                column: "IdRegion");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_IdMember",
                table: "ProjectDetails",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_IdProject",
                table: "ProjectDetails",
                column: "IdProject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_IdRole",
                table: "ProjectDetails",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IdClient",
                table: "Projects",
                column: "IdClient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_IdMember",
                table: "TaskMembers",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_IdProject",
                table: "TaskMembers",
                column: "IdProject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "TaskMembers");

            migrationBuilder.DropTable(
                name: "RoleMembers");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
