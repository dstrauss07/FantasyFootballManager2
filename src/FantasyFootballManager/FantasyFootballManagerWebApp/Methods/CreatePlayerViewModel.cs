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
        private readonly IRankingRepository _rankingRepository;

        public CreatePlayerViewModels(IRankingRepository rankingRepository)
        {
            _rankingRepository = rankingRepository;
        }
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

        public async Task<List<PlayerRankingModel>> CreatePlayerViewModel(IRankingRepository rankingRepository, IPlayerRepository playerRepository, int profileID)
        {
            int defaultProfileID = 2;
            IEnumerable<Player> allPlayers = await playerRepository.ListAllAsync();
            List<PlayerRankingModel> playerRankingModelList = new List<PlayerRankingModel>();

            foreach (Player player in allPlayers)
            {
                PlayerRankingModel playerRankingModelToAdd = new PlayerRankingModel();
                try
                {
                    PlayerRanking PR = await rankingRepository.GetByPlayerIdAsync(player.PlayerId);
                    if (PR.TestUserProfileId == profileID)
                    {
                        playerRankingModelToAdd.playerRanking = PR;
                        playerRankingModelToAdd.playerToRank = player;
                        playerRankingModelList.Add(playerRankingModelToAdd);
                    }

                }
                catch
                {
                    PlayerRanking PR = new PlayerRanking
                    {
                        PlayerId = player.PlayerId
                    };
                    playerRankingModelToAdd.playerRanking = PR;
                    playerRankingModelToAdd.playerToRank = player;
                    playerRankingModelList.Add(playerRankingModelToAdd);
                }
            }
           
            if(playerRankingModelList.Count > 0)
            {
                return playerRankingModelList;
            }

            
            else
            {
              
                foreach (Player player in allPlayers)
                {
                    PlayerRankingModel playerRankingModelToAdd = new PlayerRankingModel();
                    try
                    {
                        PlayerRanking PR = await rankingRepository.GetByPlayerIdAsync(player.PlayerId);
                        if (PR.TestUserProfileId == defaultProfileID)
                        {
                            PR.TestUserProfileId = profileID;
                            playerRankingModelToAdd.playerRanking = PR;
                            await _rankingRepository.AddAsync(PR);
                        }

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
