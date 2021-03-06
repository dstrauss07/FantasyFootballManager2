﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StraussDa.FantasyFootballLibrary;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    [DbContext(typeof(PlayerDbContext))]
    [Migration("20190805030231_updatedUserProfileProps")]
    partial class updatedUserProfileProps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.AuctionTeam", b =>
                {
                    b.Property<int>("AuctionTeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsComplete");

                    b.Property<bool>("IsPractice");

                    b.Property<int>("RemainingBudget");

                    b.Property<int>("StartingBudget");

                    b.Property<int>("TeamSize");

                    b.Property<int>("TestUserProfileId");

                    b.HasKey("AuctionTeamId");

                    b.HasIndex("TestUserProfileId");

                    b.ToTable("AuctionTeam");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.DraftTeam", b =>
                {
                    b.Property<int>("DraftTeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsPractice");

                    b.Property<string>("ScoringFormat");

                    b.Property<int>("TeamSize");

                    b.Property<int>("TestUserProfileId");

                    b.HasKey("DraftTeamId");

                    b.HasIndex("TestUserProfileId");

                    b.ToTable("DraftTeam");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlayerName");

                    b.Property<string>("PlayerPos");

                    b.Property<string>("PlayerTeam");

                    b.HasKey("PlayerId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerAuctioned", b =>
                {
                    b.Property<int>("PlayerAuctionedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuctionTeamId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("WinningBid");

                    b.HasKey("PlayerAuctionedId");

                    b.HasIndex("AuctionTeamId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerAuctioned");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerDrafted", b =>
                {
                    b.Property<int>("PlayerDraftedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DraftTeamId");

                    b.Property<int>("DraftedRound");

                    b.Property<int>("OverallPick");

                    b.Property<int>("PlayerId");

                    b.Property<int>("RoundPick");

                    b.HasKey("PlayerDraftedId");

                    b.HasIndex("DraftTeamId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerDrafted");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerRanking", b =>
                {
                    b.Property<int>("PlayerRankingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DynastyPosRank");

                    b.Property<int>("DynastyRank");

                    b.Property<int>("PlayerId");

                    b.Property<int>("PlayerRank");

                    b.Property<int>("PosRank");

                    b.Property<int>("PprPosRank");

                    b.Property<int>("PprRank");

                    b.Property<int>("TestUserProfileId");

                    b.Property<bool>("isDefault");

                    b.HasKey("PlayerRankingId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TestUserProfileId");

                    b.ToTable("PlayerRanking");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.TestUserProfile", b =>
                {
                    b.Property<int>("TestUserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("UserEmail");

                    b.Property<string>("UserPassword");

                    b.HasKey("TestUserProfileId");

                    b.ToTable("TestUserProfile");
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.AuctionTeam", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.TestUserProfile", "TestUserProfie")
                        .WithMany()
                        .HasForeignKey("TestUserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.DraftTeam", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.TestUserProfile", "TestUserProfie")
                        .WithMany()
                        .HasForeignKey("TestUserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerAuctioned", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.AuctionTeam", "AuctionTeam")
                        .WithMany("PlayersAuctioned")
                        .HasForeignKey("AuctionTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StraussDa.FantasyFootballLibrary.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerDrafted", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.DraftTeam", "DraftTeam")
                        .WithMany("PlayersDrafted")
                        .HasForeignKey("DraftTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StraussDa.FantasyFootballLibrary.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerRanking", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StraussDa.FantasyFootballLibrary.TestUserProfile", "TestUserProfie")
                        .WithMany()
                        .HasForeignKey("TestUserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
