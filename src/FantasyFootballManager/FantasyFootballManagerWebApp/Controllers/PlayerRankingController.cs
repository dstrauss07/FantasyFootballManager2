using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using FantasyFootballManagerWebApp.Models;

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

            return View(playerRankingModelList);
        }


        public async Task<IActionResult> MoveUp(PlayerRanking playerRanking)
        {
            try
            {

                PlayerRanking playerMovingDown = await _rankingRepository.FindPreviousPlayer(playerRanking);
                await _rankingRepository.UpdateAsync(playerMovingDown);
                PlayerRanking playerMovingUp = playerRanking;
                playerMovingUp.PlayerRank -= 1;
                await _rankingRepository.UpdateAsync(playerMovingUp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return RedirectToAction(nameof(Index));
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
