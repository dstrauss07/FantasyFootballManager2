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
        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> MoveUp(int id, string scoring)
        {
            int direction = 1;
            string rankType;
            try
            {
                if (scoring == "standard")
                {
                    rankType = "PlayerRank";

                }
                PlayerRanking playerRankingToChange = await _rankingRepository.GetByIdAsync(id);
                var rankTypeToCheck = playerRankingToChange.GetType().GetProperty(rankType);
                if (playerRankingToChange.GetValue(rankType) > 1)
                {
                    await ChangeStandardPlayerRanks(direction, playerRankingToChange);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> MoveDown(int id, string scoring)
        {
            int direction = -1;
            try
            {
                PlayerRanking playerRankingToChange = await _rankingRepository.GetByIdAsync(id);
                IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
                int maxPlayerRank = allPlayerRanks.Max(x => x.PlayerRank);
                if (playerRankingToChange.PlayerRank < maxPlayerRank)
                {
                    await ChangeStandardPlayerRanks(direction, playerRankingToChange);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return RedirectToAction(nameof(Index));
        }


        private async Task ChangeStandardPlayerRanks(int direction, PlayerRanking playerRankingToChange)
        {
            PlayerRanking otherPlayerRankMoving = await _rankingRepository.ChangeOtherPlayerRank(playerRankingToChange, direction);
            playerRankingToChange.PlayerRank -= direction;
            Player playerToChange = await _playerRepository.GetByIdAsync(playerRankingToChange.PlayerId);
            Player otherPlayer = await _playerRepository.GetByIdAsync(otherPlayerRankMoving.PlayerId);
            if (playerToChange.PlayerPos == otherPlayer.PlayerPos)
            {
                playerRankingToChange.PosRank -= direction;
                otherPlayerRankMoving.PosRank += direction;
            }
            await _rankingRepository.UpdateAsync(otherPlayerRankMoving);
            await _rankingRepository.UpdateAsync(playerRankingToChange);
        }



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
}
