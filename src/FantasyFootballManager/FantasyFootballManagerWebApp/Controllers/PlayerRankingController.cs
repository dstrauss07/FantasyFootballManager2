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

        public async Task<IActionResult> Sflex()
        {
            List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
            return View(playerRankingModelList.OrderBy(p => p.playerRanking.SflexRank));
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
                if (scoring == "Ppr" || scoring == "Sflex")
                {
                    PlayerRankingList[0].PprPosRank -= direction;
                    PlayerRankingList[1].PprPosRank += direction;
                }

                await _rankingRepository.UpdateAsync(PlayerRankingList[0]);
                await _rankingRepository.UpdateAsync(PlayerRankingList[1]);
            }
        }

        //public async Task<IActionResult> MoveDown(int id, string scoring)
        //{
        //    int direction = -1;
        //    try
        //    {
        //        PlayerRanking playerRankingToChange = await _rankingRepository.GetByIdAsync(id);
        //        IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
        //        int maxPlayerRank = allPlayerRanks.Max(x => x.PlayerRank);
        //        int maxPprRank = allPlayerRanks.Max(x => x.PprRank);
        //        int maxSflexRank = allPlayerRanks.Max(x => x.SflexRank);


        //        if (playerRankingToChange.PlayerRank < maxPlayerRank && scoring == "standard")
        //        {
        //            await ChangePlayerRanks(direction, playerRankingToChange, scoring);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        if (playerRankingToChange.PprRank < maxPprRank && scoring == "ppr")
        //        {
        //            await ChangePlayerRanks(direction, playerRankingToChange, scoring);
        //            return RedirectToAction(nameof(Ppr));
        //        }
        //        if (playerRankingToChange.SflexRank < maxSflexRank && scoring == "sflex")
        //        {
        //            await ChangePlayerRanks(direction, playerRankingToChange, scoring);
        //            return RedirectToAction(nameof(Sflex));
        //        }

        //    }
        //    catch
        //    {
        //        //todo log exception
        //    }
        //    return RedirectToAction(nameof(Index));
        //}





        //// GET: PlayerRanking/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PlayerRanking/Create
        //public ActionResult Create()
        //{
        //    return View(new PlayerRanking());
        //}


    }
