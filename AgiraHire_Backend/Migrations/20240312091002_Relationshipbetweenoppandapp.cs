using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiraHire_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Relationshipbetweenoppandapp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Opportunities_OpportunityId",
                table: "Applicants");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Opportunities_OpportunityId",
                table: "Applicants",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Opportunity_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Opportunities_OpportunityId",
                table: "Applicants");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Applicants_OpportunityId",
                table: "Applicants",
                column: "OpportunityId",
                principalTable: "Applicants",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
