using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class ReaAddingPropsForPlayerRanking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerRiskLevel",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PprPosRank",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PprRank",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PprValue",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SflexRank",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SflexValue",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isDefault",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerRiskLevel",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "PprPosRank",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "PprRank",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "PprValue",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "SflexRank",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "SflexValue",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "isDefault",
                table: "PlayerRanking");
        }
    }
}
