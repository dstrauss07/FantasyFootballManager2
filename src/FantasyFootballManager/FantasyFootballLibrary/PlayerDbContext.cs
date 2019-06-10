using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
        {

        }

        public DbSet<AuctionTeam> AuctionTeam { get; set; }
        public DbSet<DraftTeam> DraftTeam { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerAuctioned> PlayerAuctioned { get; set; }
        public DbSet<PlayerDrafted> PlayerDrafted { get; set; }
        public DbSet<PlayerRanking> PlayerRanking { get; set; }
        public DbSet<TestUserProfile> TestUserProfile { get; set; }
    }
}
