using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscriminatorToScheduledWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId1",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId1",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workout_type",
                table: "Workouts",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "workout_type",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Workouts",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId1",
                table: "Workouts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId1",
                table: "Workouts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
