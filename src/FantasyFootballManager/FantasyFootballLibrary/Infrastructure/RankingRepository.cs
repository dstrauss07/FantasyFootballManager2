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
        public RankingRepository(PlayerDbContext playerDbContext) : base(playerDbContext)
        {

        }
        public virtual async Task<PlayerRanking> GetByPlayerIdAsync(int id)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PlayerId == id);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<PlayerRanking> GetByPlayerRankAsync(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PlayerRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<PlayerRanking> GetByPlayerPprRankAsync(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PprRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<PlayerRanking> GetByPlayerSflexRankAsync(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.SflexRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PlayerRanking>> SwapPlayerRanks(PlayerRanking playerRanking, int i, string scoring)
        {
            List<PlayerRanking> playerRanksToReturn = new List<PlayerRanking>();
            try
            {
                if (scoring == "Standard")
                {
                    PlayerRanking otherPlayerRanking = await GetByPlayerRankAsync(playerRanking.PlayerRank - i);
                    otherPlayerRanking.PlayerRank += i;
                    playerRanking.PlayerRank -= i;
                    await UpdateAsync(otherPlayerRanking);
                    await UpdateAsync(playerRanking);
                    playerRanksToReturn.Add(playerRanking);
                    playerRanksToReturn.Add(otherPlayerRanking);
                    return playerRanksToReturn;
                }
                if (scoring == "Ppr")
                {
                    PlayerRanking otherPlayerRanking = await GetByPlayerPprRankAsync(playerRanking.PprRank - i);
                    otherPlayerRanking.PprRank += i;
                    playerRanking.PprRank -= i;
                    await UpdateAsync(otherPlayerRanking);
                    await UpdateAsync(playerRanking);
                    playerRanksToReturn.Add(playerRanking);
                    playerRanksToReturn.Add(otherPlayerRanking);
                    return playerRanksToReturn;
                }
                if (scoring == "Sflex")
                {
                    PlayerRanking otherPlayerRanking = await GetByPlayerSflexRankAsync(playerRanking.SflexRank - i);
                    otherPlayerRanking.SflexRank += i;
                    playerRanking.SflexRank -= i;
                    await UpdateAsync(otherPlayerRanking);
                    await UpdateAsync(playerRanking);
                    playerRanksToReturn.Add(playerRanking);
                    playerRanksToReturn.Add(otherPlayerRanking);
                    return playerRanksToReturn;
                }
            }
            catch(Exception ex)
            {
                    Console.WriteLine("Error, no Player Found: " + ex.Message);
                         
            }
            return null;
     
        
        }
    }
}
