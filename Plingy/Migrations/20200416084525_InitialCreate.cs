using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Plingy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    ActiveStudent = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AllergiesID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<int>(nullable: false),
                    Allergy = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllergiesID);
                    table.ForeignKey(
                        name: "FK_Allergies_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_StudentID",
                table: "Allergies",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
