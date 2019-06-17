using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class tryingTofixRanks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PlayerRanking");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: false);

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
        }
    }
}
