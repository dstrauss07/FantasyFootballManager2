using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;
using System.Linq;

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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.PlayerId == id);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.PlayerRank == rank);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.PprRank == rank);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.DynastyRank == rank);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.PosRank == rank);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.PprPosRank == rank);
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
                var PlayerRankToReturn = await selectedRepository.PlayerRanking.FirstAsync(x => x.DynastyPosRank == rank);
                return PlayerRankToReturn;
            }
            catch
            {
                return null;
            }
        }
        
        public List<PlayerRanking> CreateListOfPlayersOfPosition(PlayerRanking playerToMove, IEnumerable<PlayerRanking> allPlayerRanks, IEnumerable<Player> allPlayers)
        {
            List<PlayerRanking> playersOfPosition = new List<PlayerRanking>();
            Player origPlayer = allPlayers.First(x => x.PlayerId == playerToMove.PlayerId);
            string posTocheck = origPlayer.PlayerPos;
            foreach (PlayerRanking p in allPlayerRanks)
            {
                Player playerToCheck = allPlayers.First(x => x.PlayerId == p.PlayerId);
                if (playerToCheck.PlayerPos == posTocheck)
                {
                    playersOfPosition.Add(p);
                }
            }
            return playersOfPosition;
        }

        public async Task<List<PlayerRanking>> SwapPlayerRanks(PlayerRanking playerRanking, int i, string scoring, string playerPosition)
        {
            List<PlayerRanking> playerRanksToReturn = new List<PlayerRanking>();
            if (playerPosition == "All Players")
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

            if (playerPosition != "All Players")
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

        public async Task UpdatePosRanks(PlayerRanking playerOne, PlayerRanking playerTwo, string scoring, int direction)
        {

            if (scoring == "Standard")
            {
                playerOne.PosRank -= direction;
                playerTwo.PosRank += direction;
            }
            if (scoring == "Ppr")
            {
                playerOne.PprPosRank -= direction;
                playerTwo.PprPosRank += direction;
            }
            if (scoring == "Dynasty")
            {
                playerOne.DynastyPosRank -= direction;
                playerTwo.DynastyPosRank += direction;
            }

            await UpdateAsync(playerOne);
            await UpdateAsync(playerTwo);
        }

        public async Task MoveToTop(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove)
        {
            try
            {
                if (playerPosition == "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.PlayerRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PosRank);

                        foreach (PlayerRanking p in allPlayerRanks.Where(p => p.PlayerRank < playerToMove.PlayerRank).OrderBy(p => p.PlayerRank))
                        {
                            p.PlayerRank += 1;
                            if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                            {
                                p.PosRank += 1;
                            }
                            await UpdateAsync(p);
                        }
                        playerToMove.PlayerRank = highestRank;
                        playerToMove.PosRank = highestPosRank;
                        await UpdateAsync(playerToMove);


                    }
                    if (scoring == "Ppr")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.PprRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PprPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank < playerToMove.PprRank)
                            {
                                p.PprRank += 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.PprPosRank += 1;
                                }
                                await UpdateAsync(p);
                            }
                        }
                        playerToMove.PprRank = highestRank;
                        playerToMove.PprPosRank = highestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                    if (scoring == "Dynasty")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.DynastyRank);
                        int highestPosRank = playersOfPosition.Min(x => x.DynastyPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.DynastyRank < playerToMove.DynastyRank)
                            {
                                p.DynastyRank += 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.DynastyPosRank += 1;
                                }
                                await UpdateAsync(p);
                            }
                        }
                        playerToMove.DynastyRank = highestRank;
                        playerToMove.DynastyPosRank = highestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                }
                if (playerPosition != "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int origPosRank = playerToMove.PosRank;
                        int origPlayerRank = playerToMove.PlayerRank;
                        int highestRank = playersOfPosition.Min(x => x.PlayerRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PosRank);
                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PosRank < origPosRank).OrderByDescending(p => p.PosRank))
                        {
                            int otherPlayerRank = p.PlayerRank;
                            int otherPosRank = p.PosRank;
                            p.PlayerRank = origPlayerRank;
                            p.PosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await UpdateAsync(p);
                        }
                        playerToMove.PlayerRank = highestRank;
                        playerToMove.PosRank = highestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                    if (scoring == "Ppr")
                    {
                        int origPosRank = playerToMove.PprPosRank;
                        int origPlayerRank = playerToMove.PprRank;
                        int highestRank = playersOfPosition.Min(x => x.PprRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PprPosRank);

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PprPosRank < origPosRank).OrderByDescending(p => p.PprPosRank))
                        {
                            int otherPlayerRank = p.PprRank;
                            int otherPosRank = p.PprPosRank;
                            p.PprRank = origPlayerRank;
                            p.PprPosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await UpdateAsync(p);
                        }
                        playerToMove.PprRank = highestRank;
                        playerToMove.PprPosRank = highestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                    if (scoring == "Dynasty")
                    {
                        int origPosRank = playerToMove.DynastyPosRank;
                        int origPlayerRank = playerToMove.DynastyRank;
                        int highestRank = playersOfPosition.Min(x => x.DynastyRank);
                        int highestPosRank = playersOfPosition.Min(x => x.DynastyPosRank);

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.DynastyPosRank < origPosRank).OrderByDescending(p => p.DynastyPosRank))
                        {
                            int otherPlayerRank = p.DynastyRank;
                            int otherPosRank = p.DynastyPosRank;
                            p.DynastyRank = origPlayerRank;
                            p.DynastyPosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await UpdateAsync(p);
                        }

                        playerToMove.DynastyRank = highestRank;
                        playerToMove.DynastyPosRank = highestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                }
            }
            catch
            {
                Console.WriteLine("failure in move to bottom");
            }
        }

        public async Task MoveToBottom(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove)
        {
            try
            {
                if (playerPosition == "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.PlayerRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.PosRank);
                        foreach (PlayerRanking p in allPlayerRanks.Where(p => p.PlayerRank > playerToMove.PlayerRank).OrderBy(p => p.PlayerRank))
                        {
                            p.PlayerRank -= 1;
                            if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                            {
                                p.PosRank -= 1;
                            }
                            await UpdateAsync(p);
                        }
                        playerToMove.PlayerRank = lowestRank;
                        playerToMove.PosRank = lowestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                    if (scoring == "Ppr")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.PprRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.PprPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank > playerToMove.PprRank)
                            {
                                p.PprRank -= 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.PprPosRank -= 1;
                                }
                                await UpdateAsync(p);
                            }
                        }
                        playerToMove.PprRank = lowestRank;
                        playerToMove.PprPosRank = lowestPosRank;
                        await UpdateAsync(playerToMove);
                    }

                    if (scoring == "Dynasty")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.DynastyRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.DynastyPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.DynastyRank > playerToMove.DynastyRank)
                            {
                                p.DynastyRank -= 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.DynastyPosRank -= 1;
                                }
                                await UpdateAsync(p);
                            }
                        }
                        playerToMove.DynastyRank = lowestRank;
                        playerToMove.DynastyPosRank = lowestPosRank;
                        await UpdateAsync(playerToMove);
                    }
                }
                if (playerPosition != "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int playersPosRank = playerToMove.PosRank;
                        int origPlayerRank = playerToMove.PlayerRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PosRank > playersPosRank).OrderBy(p => p.PosRank))
                        {
                            int otherPlayerRank = p.PlayerRank;
                            p.PlayerRank = origPlayerRank;
                            p.PosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await UpdateAsync(p);
                            posMovements++;
                        }
                        playerToMove.PlayerRank = origPlayerRank;
                        playerToMove.PosRank += posMovements;
                        await UpdateAsync(playerToMove);
                    }
                    if (scoring == "Ppr")
                    {
                        int playersPosRank = playerToMove.PprPosRank;
                        int origPlayerRank = playerToMove.PprRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PprPosRank > playersPosRank).OrderBy(p => p.PprPosRank))
                        {
                            int otherPlayerRank = p.PprRank;
                            p.PprRank = origPlayerRank;
                            p.PprPosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await UpdateAsync(p);
                            posMovements++;
                        }
                        playerToMove.PprRank = origPlayerRank;
                        playerToMove.PprPosRank += posMovements;
                        await UpdateAsync(playerToMove);
                    }

                    if (scoring == "Dynasty")
                    {
                        int playersPosRank = playerToMove.DynastyPosRank;
                        int origPlayerRank = playerToMove.DynastyRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.DynastyPosRank > playersPosRank).OrderBy(p => p.DynastyPosRank))
                        {
                            int otherPlayerRank = p.DynastyRank;
                            p.DynastyRank = origPlayerRank;
                            p.DynastyPosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await UpdateAsync(p);
                            posMovements++;
                        }

                        playerToMove.DynastyRank = origPlayerRank;
                        playerToMove.DynastyPosRank += posMovements;
                        await UpdateAsync(playerToMove);
                    }
                }
            }
            catch
            {
                Console.WriteLine("failure in move to bottom");
            }
        }
    }
}
