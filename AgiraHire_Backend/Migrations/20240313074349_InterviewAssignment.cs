using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiraHire_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InterviewAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Applicants_ApplicantId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Users_InterviewerId",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_InterviewerId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_InterviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ApplicantId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ApplicantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "FeedbackId");

            migrationBuilder.CreateTable(
                name: "InterviewAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_InterviewAssignments_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewAssignments_InterviewSlots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "InterviewSlots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewAssignments_ApplicantId",
                table: "InterviewAssignments",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewAssignments_SlotId",
                table: "InterviewAssignments",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Applicants_ApplicantId",
                table: "Feedbacks",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_InterviewerId",
                table: "Feedbacks",
                column: "InterviewerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Applicants_ApplicantId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_InterviewerId",
                table: "Feedbacks");

            migrationBuilder.DropTable(
                name: "InterviewAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_InterviewerId",
                table: "Feedback",
                newName: "IX_Feedback_InterviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ApplicantId",
                table: "Feedback",
                newName: "IX_Feedback_ApplicantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Applicants_ApplicantId",
                table: "Feedback",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Users_InterviewerId",
                table: "Feedback",
                column: "InterviewerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
