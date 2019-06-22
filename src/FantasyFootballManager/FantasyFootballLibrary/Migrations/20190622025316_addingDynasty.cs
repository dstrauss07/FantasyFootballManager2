using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class addingDynasty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerRiskLevel",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "PlayerValue",
                table: "PlayerRanking");

            migrationBuilder.DropColumn(
                name: "PprValue",
                table: "PlayerRanking");

            migrationBuilder.RenameColumn(
                name: "SflexValue",
                table: "PlayerRanking",
                newName: "DynastyPosRank");

            migrationBuilder.RenameColumn(
                name: "SflexRank",
                table: "PlayerRanking",
                newName: "DynastyRank");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DynastyRank",
                table: "PlayerRanking",
                newName: "SflexRank");

            migrationBuilder.RenameColumn(
                name: "DynastyPosRank",
                table: "PlayerRanking",
                newName: "SflexValue");

            migrationBuilder.AddColumn<int>(
                name: "PlayerRiskLevel",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PlayerValue",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PprValue",
                table: "PlayerRanking",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
