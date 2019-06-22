using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using FantasyFootballManagerWebApp.Models;
using System.Reflection;

namespace FantasyFootballManagerWebApp.Controllers
{
    public class PlayerRankingController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;

        public PlayerRankingController(IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
        }




        // GET: PlayerRanking
        public async Task<IActionResult> Standard()
        {
            List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
            return View(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
        }

        public async Task<IActionResult> Ppr()
        {
            List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
            return View(playerRankingModelList.OrderBy(p => p.playerRanking.PprRank));
        }

        public async Task<IActionResult> Dynasty()
        {
            List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
            return View(playerRankingModelList.OrderBy(p => p.playerRanking.DynastyRank));
        }


        private async Task<List<PlayerRankingModel>> CreatePlayerViewModel()
        {
            IEnumerable<Player> allPlayers = await _playerRepository.ListAllAsync();
            List<PlayerRankingModel> playerRankingModelList = new List<PlayerRankingModel>();

            foreach (Player player in allPlayers)
            {
                PlayerRankingModel playerRankingModelToAdd = new PlayerRankingModel();
                try
                {
                    PlayerRanking PR = await _rankingRepository.GetByPlayerIdAsync(player.PlayerId);
                    playerRankingModelToAdd.playerRanking = PR;
                }
                catch
                {
                    PlayerRanking PR = new PlayerRanking();
                    PR.PlayerId = player.PlayerId;
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

        public async Task<IActionResult> MovePlayers(int id, string scoring, int direction)
        {
            try
            {
                PlayerRanking playerRankingToChange = await _rankingRepository.GetByIdAsync(id);
                List<PlayerRanking> playerRankingList = await _rankingRepository.SwapPlayerRanks(playerRankingToChange, direction, scoring);
                Player playerToChange = await _playerRepository.GetByIdAsync(playerRankingList[0].PlayerId);
                Player otherPlayer = await _playerRepository.GetByIdAsync(playerRankingList[1].PlayerId);
                if(playerToChange.PlayerPos == otherPlayer.PlayerPos)
                {
                    await UpdatePosRanks(playerRankingList, scoring, direction);
                }
                       return RedirectToAction(scoring);
   
            }
            catch
            {
                //todo log exception
            }
            return RedirectToAction(scoring);
        }


        private async Task UpdatePosRanks(List<PlayerRanking> PlayerRankingList, String scoring, int direction)
        {

                if (scoring == "Standard")
                {
                    PlayerRankingList[0].PosRank -= direction;
                    PlayerRankingList[1].PosRank += direction;
                }
                if (scoring == "Ppr" )
                {
                    PlayerRankingList[0].PprPosRank -= direction;
                    PlayerRankingList[1].PprPosRank += direction;
                }
            if (scoring == "Dynasty")
            {
                PlayerRankingList[0].DynastyPosRank -= direction;
                PlayerRankingList[1].DynastyPosRank += direction;
            }

            await _rankingRepository.UpdateAsync(PlayerRankingList[0]);
                await _rankingRepository.UpdateAsync(PlayerRankingList[1]);
            }
        }


    }
