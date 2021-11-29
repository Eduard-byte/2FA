using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mvc.Server.Migrations
{
    public partial class BranchAndGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValues_AspNetUsers_CreatedUserId",
                table: "ParameterValues");

            migrationBuilder.DropIndex(
                name: "IX_ParameterValues_CreatedUserId",
                table: "ParameterValues");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "ParameterValues");

            migrationBuilder.AddColumn<string>(
                name: "NameArab",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventParameterGroupId",
                table: "EventParameters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventParameterGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParameterGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventPrograms_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PriceRuble = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceDollar = table.Column<decimal>(type: "numeric", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EventProgramId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramPoints_EventPrograms_EventProgramId",
                        column: x => x.EventProgramId,
                        principalTable: "EventPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageProgramPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramPointId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageProgramPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageProgramPoints_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageProgramPoints_ProgramPoints_ProgramPointId",
                        column: x => x.ProgramPointId,
                        principalTable: "ProgramPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPointBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPointBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramPointBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramPointBranches_ProgramPoints_ProgramPointId",
                        column: x => x.ProgramPointId,
                        principalTable: "ProgramPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPointLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramPointId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPointLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramPointLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramPointLocations_ProgramPoints_ProgramPointId",
                        column: x => x.ProgramPointId,
                        principalTable: "ProgramPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusStories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusStories_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestStatusStories_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParameters_EventParameterGroupId",
                table: "EventParameters",
                column: "EventParameterGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_EventId",
                table: "Branches",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPrograms_EventId",
                table: "EventPrograms",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProgramPoints_PackageId",
                table: "PackageProgramPoints",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProgramPoints_ProgramPointId",
                table: "PackageProgramPoints",
                column: "ProgramPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_EventId",
                table: "Packages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPointBranches_BranchId",
                table: "ProgramPointBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPointBranches_ProgramPointId",
                table: "ProgramPointBranches",
                column: "ProgramPointId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPointLocations_LocationId",
                table: "ProgramPointLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPointLocations_ProgramPointId",
                table: "ProgramPointLocations",
                column: "ProgramPointId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPoints_EventProgramId",
                table: "ProgramPoints",
                column: "EventProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedUserId",
                table: "Requests",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_PackageId",
                table: "Requests",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusStories_RequestId",
                table: "RequestStatusStories",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusStories_StatusId",
                table: "RequestStatusStories",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParameters_EventParameterGroups_EventParameterGroupId",
                table: "EventParameters",
                column: "EventParameterGroupId",
                principalTable: "EventParameterGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParameters_EventParameterGroups_EventParameterGroupId",
                table: "EventParameters");

            migrationBuilder.DropTable(
                name: "EventParameterGroups");

            migrationBuilder.DropTable(
                name: "PackageProgramPoints");

            migrationBuilder.DropTable(
                name: "ProgramPointBranches");

            migrationBuilder.DropTable(
                name: "ProgramPointLocations");

            migrationBuilder.DropTable(
                name: "RequestStatusStories");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "ProgramPoints");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "EventPrograms");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_EventParameters_EventParameterGroupId",
                table: "EventParameters");

            migrationBuilder.DropColumn(
                name: "NameArab",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventParameterGroupId",
                table: "EventParameters");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "ParameterValues",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_CreatedUserId",
                table: "ParameterValues",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValues_AspNetUsers_CreatedUserId",
                table: "ParameterValues",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
