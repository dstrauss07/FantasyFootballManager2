using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class updatingRankingentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCustom",
                table: "PlayerRanking",
                newName: "IsDefault");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "PlayerRanking",
                newName: "IsCustom");
        }
    }
}
