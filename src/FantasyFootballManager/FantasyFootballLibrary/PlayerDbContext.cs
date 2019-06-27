using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        internal Task<PlayerRanking> GetByPlayerRankAsync(int v)
        {
            throw new NotImplementedException();
        }

        internal Task UpdateAsync(PlayerRanking otherPlayerRanking)
        {
            throw new NotImplementedException();
        }
    }
}
