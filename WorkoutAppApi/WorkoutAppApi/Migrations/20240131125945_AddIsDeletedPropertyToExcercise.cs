using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedPropertyToExcercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Excercises",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Excercises");
        }
    }
}
