using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using FantasyFootballManagerWebApp.Models;

namespace FantasyFootballManagerWebApp.Methods
{
    public class CreatePlayerViewModels
    {

        public async Task<List<PlayerRankingModel>> CreatePlayerViewModel(IRankingRepository rankingRepository, IPlayerRepository playerRepository)
        {
            IEnumerable<Player> allPlayers = await playerRepository.ListAllAsync();
            List<PlayerRankingModel> playerRankingModelList = new List<PlayerRankingModel>();

            foreach (Player player in allPlayers)
            {
                PlayerRankingModel playerRankingModelToAdd = new PlayerRankingModel();
                try
                {
                    PlayerRanking PR = await rankingRepository.GetByPlayerIdAsync(player.PlayerId);
                    playerRankingModelToAdd.playerRanking = PR;
                }
                catch
                {
                    PlayerRanking PR = new PlayerRanking
                    {
                        PlayerId = player.PlayerId
                    };
                    playerRankingModelToAdd.playerRanking = PR;
                }
                finally
                {
                    playerRankingModelToAdd.playerToRank = player;
                    playerRankingModelList.Add(playerRankingModelToAdd);
                }
            }
            return playerRankingModelList;
        }


        public async Task<List<PlayerRankingModel>> CreatePlayerViewModel(string position, IRankingRepository rankingRepository, IPlayerRepository playerRepository)
        {
            IEnumerable<Player> allPlayers = await playerRepository.ListAllAsync();
            List<PlayerRankingModel> playerRankingModelList = new List<PlayerRankingModel>();

            foreach (Player player in allPlayers.Where(p => p.PlayerPos == position))
            {
                PlayerRankingModel playerRankingModelToAdd = new PlayerRankingModel();
                try
                {
                    PlayerRanking PR = await rankingRepository.GetByPlayerIdAsync(player.PlayerId);

                    playerRankingModelToAdd.playerRanking = PR;
                }
                catch
                {
                    PlayerRanking PR = new PlayerRanking
                    {
                        PlayerId = player.PlayerId
                    };
                    playerRankingModelToAdd.playerRanking = PR;
                }
                finally
                {
                    playerRankingModelToAdd.playerToRank = player;
                    playerRankingModelList.Add(playerRankingModelToAdd);
                }
            }


            return playerRankingModelList;
        }
    }
}
