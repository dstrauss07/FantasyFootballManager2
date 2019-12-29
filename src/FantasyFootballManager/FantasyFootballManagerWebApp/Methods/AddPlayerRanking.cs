using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;

namespace FantasyFootballManagerWebApp.Methods
{
    public class AddPlayerRankingMethod
    {

        public static async Task AddPlayerRanking(Player returnedPlayer, IRankingRepository rankingRepository, IPlayerRepository playerRepository)
        {
            if (await rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId) != null)
            {
                PlayerRanking playerRankingToDelete = await rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId);
                await rankingRepository.DeleteAsync(playerRankingToDelete);
                Console.Write("Previous Ranking Deleted");
            }

            PlayerRanking playerRankingToAdd = new PlayerRanking();
            playerRankingToAdd.PlayerId = returnedPlayer.PlayerId;
            IEnumerable<PlayerRanking> allPlayerRanks = await rankingRepository.ListAllAsync();
            IEnumerable<Player> allPlayers = await playerRepository.ListAllAsync();
            if (allPlayerRanks.Count() > 0)
            {
                var highestRankedPlayer = allPlayerRanks.Max(x => x.PlayerRank);
                var highestPprPlayer = allPlayerRanks.Max(x => x.PprRank);
                var highestDynastyPlayer = allPlayerRanks.Max(x => x.DynastyRank);
                playerRankingToAdd.PlayerRank = highestRankedPlayer + 1;
                playerRankingToAdd.PprRank = highestPprPlayer + 1;
                playerRankingToAdd.DynastyRank = highestDynastyPlayer + 1;
                CalculatePlayerRanking(returnedPlayer, allPlayerRanks, allPlayers, playerRankingToAdd);
            }
            else
            {
                playerRankingToAdd.PlayerRank = 1;
                playerRankingToAdd.PosRank = 1;
                playerRankingToAdd.PprRank = 1;
                playerRankingToAdd.PprPosRank = 1;
                playerRankingToAdd.DynastyRank = 1;
                playerRankingToAdd.DynastyPosRank = 1;
            }

            playerRankingToAdd.TestUserProfileId = 2025;
            playerRankingToAdd.isDefault = true;
            await rankingRepository.AddAsync(playerRankingToAdd);
        }

        private static void CalculatePlayerRanking(Player returnedPlayer, IEnumerable<PlayerRanking> allPlayerRankings, IEnumerable<Player> allPlayers, PlayerRanking playerRankingToAdd )
        {
            List<PlayerRanking> allPosRanks = new List<PlayerRanking>();
            foreach(PlayerRanking p in allPlayerRankings)
            {
                var idToCheck = p.PlayerId;
                Player playerToCheck = allPlayers.First(x => x.PlayerId == idToCheck);
                if(playerToCheck.PlayerPos == returnedPlayer.PlayerPos)
                {
                    allPosRanks.Add(p);
                }
            }
            if (allPosRanks.Count() > 0)
            {
                var highestPosRank = allPosRanks.Max(x => x.PosRank);
                var highestPprPosRank = allPosRanks.Max(x => x.PprPosRank);
                var highestDynastyPosRank = allPosRanks.Max(x => x.DynastyPosRank);
                playerRankingToAdd.PosRank = highestPosRank + 1;
                playerRankingToAdd.PprPosRank = highestPprPosRank + 1;
                playerRankingToAdd.DynastyPosRank = highestDynastyPosRank + 1;
            }
            else
            {
                playerRankingToAdd.PosRank = 1;
                playerRankingToAdd.PprPosRank = 1;
                playerRankingToAdd.DynastyPosRank = 1;
            }

        }
    }

}