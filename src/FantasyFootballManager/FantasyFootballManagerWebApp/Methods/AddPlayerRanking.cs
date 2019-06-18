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

        public static async Task AddPlayerRanking(Player newPlayer, IPlayerRepository _playerRepository, IRankingRepository _rankingRepository)
        {
            var returnedPlayer = await _playerRepository.GetByPlayerByName(newPlayer.PlayerName);
            if (await _rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId) != null)
            {
                PlayerRanking playerRankingToDelete = await _rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId);
                await _rankingRepository.DeleteAsync(playerRankingToDelete);
                Console.Write("Previous Ranking Deleted");
            }

            PlayerRanking playerRankingToAdd = new PlayerRanking();
            playerRankingToAdd.PlayerId = returnedPlayer.PlayerId;
            IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
            if (allPlayerRanks.Count() > 0)
            {
                var highestRankedPlayer = allPlayerRanks.Max(x => x.PlayerRank);
                playerRankingToAdd.PlayerRank = highestRankedPlayer + 1;
            }
            else
            {
                playerRankingToAdd.PlayerRank = 1;
            }

            playerRankingToAdd.TestUserProfileId = 2;
            playerRankingToAdd.isDefault = true;
            await _rankingRepository.AddAsync(playerRankingToAdd);
        }
    }

}