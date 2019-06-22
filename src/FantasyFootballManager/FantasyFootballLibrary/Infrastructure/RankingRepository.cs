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
        public virtual async Task<PlayerRanking> GetByPlayerDynastyRankAsync(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.DynastyRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<PlayerRanking> GetByPlayerStandardPosRank(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PosRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<PlayerRanking> GetByPlayerPprPosRank(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.PprPosRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<PlayerRanking> GetByPlayerDynastyPosRank(int rank)
        {
            try
            {
                var PlayerRankToReturn = await _dbContext.PlayerRanking.FirstAsync(x => x.DynastyPosRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<PlayerRanking>> SwapPlayerRanks(PlayerRanking playerRanking, int i, string scoring, string playerPosition)
        {
            List<PlayerRanking> playerRanksToReturn = new List<PlayerRanking>();
            if(playerPosition == "All Players")
            {

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
                    if (scoring == "Dynasty")
                    {
                        PlayerRanking otherPlayerRanking = await GetByPlayerDynastyRankAsync(playerRanking.DynastyRank - i);
                        otherPlayerRanking.DynastyRank += i;
                        playerRanking.DynastyRank -= i;
                        await UpdateAsync(otherPlayerRanking);
                        await UpdateAsync(playerRanking);
                        playerRanksToReturn.Add(playerRanking);
                        playerRanksToReturn.Add(otherPlayerRanking);
                        return playerRanksToReturn;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error, no Player Found: " + ex.Message);

                }
                return null;

            }
     
            if(playerPosition != "All Players")
            {
                try
                {
                    if (scoring == "Standard")
                    {
                        PlayerRanking otherPlayerRanking = await GetByPlayerStandardPosRank(playerRanking.PosRank - i);
                        int otherPlayersRank = otherPlayerRanking.PlayerRank;
                        int currentPlayersRank = playerRanking.PlayerRank;
                        playerRanking.PlayerRank = otherPlayersRank;
                        otherPlayerRanking.PlayerRank = currentPlayersRank;


                        await UpdateAsync(otherPlayerRanking);
                        await UpdateAsync(playerRanking);
                        playerRanksToReturn.Add(playerRanking);
                        playerRanksToReturn.Add(otherPlayerRanking);
                        return playerRanksToReturn;
                    }
                    if (scoring == "Ppr")
                    {
                        PlayerRanking otherPlayerRanking = await GetByPlayerPprPosRank(playerRanking.PprPosRank - i);
                        int otherPlayersRank = otherPlayerRanking.PprRank;
                        int currentPlayersRank = playerRanking.PprRank;
                        playerRanking.PprRank = otherPlayersRank;
                        otherPlayerRanking.PprRank = currentPlayersRank;
                        await UpdateAsync(otherPlayerRanking);
                        await UpdateAsync(playerRanking);
                        playerRanksToReturn.Add(playerRanking);
                        playerRanksToReturn.Add(otherPlayerRanking);
                        return playerRanksToReturn;
                    }
                    if (scoring == "Dynasty")
                    {
                        PlayerRanking otherPlayerRanking = await GetByPlayerDynastyPosRank(playerRanking.DynastyPosRank - i);
                        int otherPlayersRank = otherPlayerRanking.DynastyRank;
                        int currentPlayersRank = playerRanking.DynastyRank;
                        playerRanking.DynastyRank = otherPlayersRank;
                        otherPlayerRanking.DynastyRank = currentPlayersRank;
                        await UpdateAsync(otherPlayerRanking);
                        await UpdateAsync(playerRanking);
                        playerRanksToReturn.Add(playerRanking);
                        playerRanksToReturn.Add(otherPlayerRanking);
                        return playerRanksToReturn;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error, no Player Found: " + ex.Message);

                }
                return null;

            }
            else
            {
                return null;
            }
        
        }
    }
}
