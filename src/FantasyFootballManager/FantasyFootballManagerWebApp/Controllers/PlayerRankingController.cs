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

        // GET: PlayerRanking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlayerRanking/Create
        public ActionResult Create()
        {
            return View(new PlayerRanking());
        }

        // POST: PlayerRanking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerRanking newPlayerRanking, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newPlayerRanking);
                }

                await _rankingRepository.AddAsync(newPlayerRanking);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newPlayerRanking);
        }

        // GET: PlayerRanking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlayerRanking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerRanking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlayerRanking/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}