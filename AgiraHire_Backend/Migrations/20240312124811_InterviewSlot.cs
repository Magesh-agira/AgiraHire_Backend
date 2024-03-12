using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiraHire_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InterviewSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewSlots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewerId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSlots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_InterviewSlots_Interview_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Interview_Rounds",
                        principalColumn: "RoundID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewSlots_Users_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSlots_InterviewerId",
                table: "InterviewSlots",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSlots_RoundId",
                table: "InterviewSlots",
                column: "RoundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewSlots");
        }
    }
}
