using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetManagerPrism.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvitedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitedUsers_Users_UserId",
                table: "InvitedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitedUsers_Users_UserId",
                table: "InvitedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitedUsers_Users_UserId",
                table: "InvitedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitedUsers_Users_UserId",
                table: "InvitedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
