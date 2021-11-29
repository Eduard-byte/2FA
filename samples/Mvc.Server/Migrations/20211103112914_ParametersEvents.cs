using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mvc.Server.Migrations
{
    public partial class ParametersEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Uneditable = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventParameters_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParameters_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValues_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterValues_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterValues_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParameters_EventId",
                table: "EventParameters",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParameters_ParameterId",
                table: "EventParameters",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_CreatedUserId",
                table: "ParameterValues",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_EventId",
                table: "ParameterValues",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_ParameterId",
                table: "ParameterValues",
                column: "ParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParameters");

            migrationBuilder.DropTable(
                name: "ParameterValues");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Parameters");
        }
    }
}
