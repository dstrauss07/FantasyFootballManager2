using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class UpdatedTeamLogic2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId",
                table: "PlayerDrafted");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDrafted_Player_PlayerId",
                table: "PlayerDrafted");

            migrationBuilder.DropTable(
                name: "PlayerAuctioned");

            migrationBuilder.DropTable(
                name: "AuctionTeam");

            migrationBuilder.DropIndex(
                name: "IX_PlayerDrafted_PlayerId",
                table: "PlayerDrafted");

            migrationBuilder.DropColumn(
                name: "IsPractice",
                table: "DraftTeam");

            migrationBuilder.RenameColumn(
                name: "RoundPick",
                table: "PlayerDrafted",
                newName: "WinningBid");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "PlayerDrafted",
                newName: "TeamPicked");

            migrationBuilder.RenameColumn(
                name: "OverallPick",
                table: "PlayerDrafted",
                newName: "PlayerRankingId");

            migrationBuilder.RenameColumn(
                name: "DraftedRound",
                table: "PlayerDrafted",
                newName: "PickNumber");

            migrationBuilder.RenameColumn(
                name: "TeamSize",
                table: "DraftTeam",
                newName: "TotalStartingWR");

            migrationBuilder.AlterColumn<int>(
                name: "DraftTeamId",
                table: "PlayerDrafted",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DraftTeamId1",
                table: "PlayerDrafted",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DraftSlot",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueSize",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LeagueType",
                table: "DraftTeam",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingBudget",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPlayer",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingD",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingFlex",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingK",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingQB",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingRB",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingSFlex",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStartingTE",
                table: "DraftTeam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDrafted_DraftTeamId1",
                table: "PlayerDrafted",
                column: "DraftTeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDrafted_PlayerRankingId",
                table: "PlayerDrafted",
                column: "PlayerRankingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId",
                table: "PlayerDrafted",
                column: "DraftTeamId",
                principalTable: "DraftTeam",
                principalColumn: "DraftTeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId1",
                table: "PlayerDrafted",
                column: "DraftTeamId1",
                principalTable: "DraftTeam",
                principalColumn: "DraftTeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDrafted_Player_PlayerRankingId",
                table: "PlayerDrafted",
                column: "PlayerRankingId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId",
                table: "PlayerDrafted");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId1",
                table: "PlayerDrafted");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDrafted_Player_PlayerRankingId",
                table: "PlayerDrafted");

            migrationBuilder.DropIndex(
                name: "IX_PlayerDrafted_DraftTeamId1",
                table: "PlayerDrafted");

            migrationBuilder.DropIndex(
                name: "IX_PlayerDrafted_PlayerRankingId",
                table: "PlayerDrafted");

            migrationBuilder.DropColumn(
                name: "DraftTeamId1",
                table: "PlayerDrafted");

            migrationBuilder.DropColumn(
                name: "DraftSlot",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "LeagueSize",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "LeagueType",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "StartingBudget",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalPlayer",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingD",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingFlex",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingK",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingQB",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingRB",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingSFlex",
                table: "DraftTeam");

            migrationBuilder.DropColumn(
                name: "TotalStartingTE",
                table: "DraftTeam");

            migrationBuilder.RenameColumn(
                name: "WinningBid",
                table: "PlayerDrafted",
                newName: "RoundPick");

            migrationBuilder.RenameColumn(
                name: "TeamPicked",
                table: "PlayerDrafted",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerRankingId",
                table: "PlayerDrafted",
                newName: "OverallPick");

            migrationBuilder.RenameColumn(
                name: "PickNumber",
                table: "PlayerDrafted",
                newName: "DraftedRound");

            migrationBuilder.RenameColumn(
                name: "TotalStartingWR",
                table: "DraftTeam",
                newName: "TeamSize");

            migrationBuilder.AlterColumn<int>(
                name: "DraftTeamId",
                table: "PlayerDrafted",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPractice",
                table: "DraftTeam",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AuctionTeam",
                columns: table => new
                {
                    AuctionTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsPractice = table.Column<bool>(nullable: false),
                    RemainingBudget = table.Column<int>(nullable: false),
                    StartingBudget = table.Column<int>(nullable: false),
                    TeamSize = table.Column<int>(nullable: false),
                    TestUserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionTeam", x => x.AuctionTeamId);
                    table.ForeignKey(
                        name: "FK_AuctionTeam_TestUserProfile_TestUserProfileId",
                        column: x => x.TestUserProfileId,
                        principalTable: "TestUserProfile",
                        principalColumn: "TestUserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAuctioned",
                columns: table => new
                {
                    PlayerAuctionedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionTeamId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    WinningBid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAuctioned", x => x.PlayerAuctionedId);
                    table.ForeignKey(
                        name: "FK_PlayerAuctioned_AuctionTeam_AuctionTeamId",
                        column: x => x.AuctionTeamId,
                        principalTable: "AuctionTeam",
                        principalColumn: "AuctionTeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAuctioned_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDrafted_PlayerId",
                table: "PlayerDrafted",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionTeam_TestUserProfileId",
                table: "AuctionTeam",
                column: "TestUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAuctioned_AuctionTeamId",
                table: "PlayerAuctioned",
                column: "AuctionTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAuctioned_PlayerId",
                table: "PlayerAuctioned",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDrafted_DraftTeam_DraftTeamId",
                table: "PlayerDrafted",
                column: "DraftTeamId",
                principalTable: "DraftTeam",
                principalColumn: "DraftTeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDrafted_Player_PlayerId",
                table: "PlayerDrafted",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
