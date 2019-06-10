using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerName = table.Column<string>(nullable: true),
                    PlayerPos = table.Column<string>(nullable: true),
                    PlayerTeam = table.Column<string>(nullable: true),
                    PlayerImangeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "TestUserProfile",
                columns: table => new
                {
                    TestUserProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestUserProfile", x => x.TestUserProfileId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionTeam",
                columns: table => new
                {
                    AuctionTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsPractice = table.Column<bool>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    TeamSize = table.Column<int>(nullable: false),
                    StartingBudget = table.Column<int>(nullable: false),
                    RemainingBudget = table.Column<int>(nullable: false),
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
                name: "DraftTeam",
                columns: table => new
                {
                    DraftTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsPractice = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    TeamSize = table.Column<int>(nullable: false),
                    ScoringFormat = table.Column<string>(nullable: true),
                    TestUserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftTeam", x => x.DraftTeamId);
                    table.ForeignKey(
                        name: "FK_DraftTeam_TestUserProfile_TestUserProfileId",
                        column: x => x.TestUserProfileId,
                        principalTable: "TestUserProfile",
                        principalColumn: "TestUserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRanking",
                columns: table => new
                {
                    PlayerRankingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsCustom = table.Column<bool>(nullable: false),
                    PlayerRank = table.Column<int>(nullable: false),
                    PosRank = table.Column<int>(nullable: false),
                    PlayerValue = table.Column<decimal>(nullable: false),
                    PprRank = table.Column<int>(nullable: false),
                    PprPosRank = table.Column<int>(nullable: false),
                    PprValue = table.Column<decimal>(nullable: false),
                    SflexRank = table.Column<int>(nullable: false),
                    SflexValue = table.Column<decimal>(nullable: false),
                    PlayerRiskLevel = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    TestUserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRanking", x => x.PlayerRankingId);
                    table.ForeignKey(
                        name: "FK_PlayerRanking_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRanking_TestUserProfile_TestUserProfileId",
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
                    WinningBid = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    AuctionTeamId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PlayerDrafted",
                columns: table => new
                {
                    PlayerDraftedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DraftedRound = table.Column<int>(nullable: false),
                    RoundPick = table.Column<int>(nullable: false),
                    OverallPick = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    DraftTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDrafted", x => x.PlayerDraftedId);
                    table.ForeignKey(
                        name: "FK_PlayerDrafted_DraftTeam_DraftTeamId",
                        column: x => x.DraftTeamId,
                        principalTable: "DraftTeam",
                        principalColumn: "DraftTeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerDrafted_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionTeam_TestUserProfileId",
                table: "AuctionTeam",
                column: "TestUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeam_TestUserProfileId",
                table: "DraftTeam",
                column: "TestUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAuctioned_AuctionTeamId",
                table: "PlayerAuctioned",
                column: "AuctionTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAuctioned_PlayerId",
                table: "PlayerAuctioned",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDrafted_DraftTeamId",
                table: "PlayerDrafted",
                column: "DraftTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDrafted_PlayerId",
                table: "PlayerDrafted",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRanking_PlayerId",
                table: "PlayerRanking",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRanking_TestUserProfileId",
                table: "PlayerRanking",
                column: "TestUserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAuctioned");

            migrationBuilder.DropTable(
                name: "PlayerDrafted");

            migrationBuilder.DropTable(
                name: "PlayerRanking");

            migrationBuilder.DropTable(
                name: "AuctionTeam");

            migrationBuilder.DropTable(
                name: "DraftTeam");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "TestUserProfile");
        }
    }
}
