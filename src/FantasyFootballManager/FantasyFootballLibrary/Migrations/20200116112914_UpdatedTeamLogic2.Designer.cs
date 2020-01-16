﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StraussDa.FantasyFootballLibrary;

namespace StraussDa.FantasyFootballLibrary.Migrations
{
    [DbContext(typeof(PlayerDbContext))]
    [Migration("20200116112914_UpdatedTeamLogic2")]
    partial class UpdatedTeamLogic2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.DraftTeam", b =>
                {
                    b.Property<int>("DraftTeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DraftSlot");

                    b.Property<bool>("IsCompleted");

                    b.Property<int>("LeagueSize");

                    b.Property<string>("LeagueType");

                    b.Property<string>("ScoringFormat");

                    b.Property<int>("StartingBudget");

                    b.Property<int>("TestUserProfileId");

                    b.Property<int>("TotalPlayer");

                    b.Property<int>("TotalStartingD");

                    b.Property<int>("TotalStartingFlex");

                    b.Property<int>("TotalStartingK");

                    b.Property<int>("TotalStartingQB");

                    b.Property<int>("TotalStartingRB");

                    b.Property<int>("TotalStartingSFlex");

                    b.Property<int>("TotalStartingTE");

                    b.Property<int>("TotalStartingWR");

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

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerDrafted", b =>
                {
                    b.Property<int>("PlayerDraftedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DraftTeamId");

                    b.Property<int?>("DraftTeamId1");

                    b.Property<int>("PickNumber");

                    b.Property<int>("PlayerRankingId");

                    b.Property<int>("TeamPicked");

                    b.Property<int>("WinningBid");

                    b.HasKey("PlayerDraftedId");

                    b.HasIndex("DraftTeamId");

                    b.HasIndex("DraftTeamId1");

                    b.HasIndex("PlayerRankingId");

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

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.DraftTeam", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.TestUserProfile", "TestUserProfie")
                        .WithMany()
                        .HasForeignKey("TestUserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StraussDa.FantasyFootballLibrary.PlayerDrafted", b =>
                {
                    b.HasOne("StraussDa.FantasyFootballLibrary.DraftTeam")
                        .WithMany("AllPlayersDrafted")
                        .HasForeignKey("DraftTeamId");

                    b.HasOne("StraussDa.FantasyFootballLibrary.DraftTeam")
                        .WithMany("MyPlayersDrafted")
                        .HasForeignKey("DraftTeamId1");

                    b.HasOne("StraussDa.FantasyFootballLibrary.Player", "PlayerRanking")
                        .WithMany()
                        .HasForeignKey("PlayerRankingId")
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
