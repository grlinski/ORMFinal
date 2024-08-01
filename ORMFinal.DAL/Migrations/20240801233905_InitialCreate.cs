using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORMFinal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Habitat = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStarted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "AnimalHealths",
                columns: table => new
                {
                    HealthReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVaccinationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalHealths", x => x.HealthReportId);
                    table.ForeignKey(
                        name: "FK_AnimalHealths_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exhibits",
                columns: table => new
                {
                    ExhibitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibits", x => x.ExhibitId);
                    table.ForeignKey(
                        name: "FK_Exhibits_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeedingSchedules",
                columns: table => new
                {
                    FeedingScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MorningFeeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoonFeeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EveningFeeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NightFeeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingSchedules", x => x.FeedingScheduleId);
                    table.ForeignKey(
                        name: "FK_FeedingSchedules_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExhibitEmployees",
                columns: table => new
                {
                    EmployeesEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ExhibitsExhibitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitEmployees", x => new { x.EmployeesEmployeeId, x.ExhibitsExhibitId });
                    table.ForeignKey(
                        name: "FK_ExhibitEmployees_Employees_EmployeesEmployeeId",
                        column: x => x.EmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExhibitEmployees_Exhibits_ExhibitsExhibitId",
                        column: x => x.ExhibitsExhibitId,
                        principalTable: "Exhibits",
                        principalColumn: "ExhibitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHealths_AnimalId",
                table: "AnimalHealths",
                column: "AnimalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitEmployees_ExhibitsExhibitId",
                table: "ExhibitEmployees",
                column: "ExhibitsExhibitId");

            migrationBuilder.CreateIndex(
                name: "IX_Exhibits_AnimalId",
                table: "Exhibits",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingSchedules_AnimalId",
                table: "FeedingSchedules",
                column: "AnimalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalHealths");

            migrationBuilder.DropTable(
                name: "ExhibitEmployees");

            migrationBuilder.DropTable(
                name: "FeedingSchedules");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Exhibits");

            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
