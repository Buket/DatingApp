using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dataing.API.Migrations
{
    public partial class ExtendedUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UsersSet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UsersSet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "UsersSet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "UsersSet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_UsersSet_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "UsersSet");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "UsersSet");
        }
    }
}
