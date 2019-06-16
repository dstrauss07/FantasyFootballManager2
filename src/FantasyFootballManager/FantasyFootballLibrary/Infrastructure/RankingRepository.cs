using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;

namespace StraussDa.FantasyFootballLibrary.Infrastructure
{
    public class RankingRepository : EfRepository<PlayerRanking>, IRankingRepository
    {
        public RankingRepository(PlayerDbContext playerDbContext): base(playerDbContext)
        {

        }
        public virtual async Task<PlayerRanking> GetByPlayerIdAsync(int id)
        {
             var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PlayerId == id);
            return PlayerRankToReturn;
    }
    }
}
