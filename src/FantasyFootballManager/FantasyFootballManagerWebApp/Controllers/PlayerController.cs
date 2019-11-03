using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using MoreLinq;
using FantasyFootballManagerWebApp.Methods;



namespace FantasyFootballManagerWebApp.Controllers
{
    public class PlayerController : Controller
    {

        private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;

        public PlayerController(IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            var listOfPlayer = await _playerRepository.ListAllAsync();
            return View(listOfPlayer);
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var playerDetails = await _playerRepository.GetByIdAsync(id);
            return View(playerDetails);
        }

        // GET: Player/Create
        public ActionResult Create()
        {

            return View(new Player());
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player newPlayer, IFormCollection collection)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newPlayer);
                }

                await _playerRepository.AddNewPlayerAsync(newPlayer);
                var returnedPlayer = await _playerRepository.GetByPlayerByName(newPlayer.PlayerName);
                await AddPlayerRankingMethod.AddPlayerRanking(returnedPlayer, _rankingRepository, _playerRepository);

                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newPlayer);
        }

        //private async Task AddPlayerRanking(Player newPlayer)
        //{
        //    var returnedPlayer = await _playerRepository.GetByPlayerByName(newPlayer.PlayerName);
        //    if (await _rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId) != null)
        //    {
        //        PlayerRanking playerRankingToDelete = await _rankingRepository.GetByPlayerIdAsync(returnedPlayer.PlayerId);
        //        await _rankingRepository.DeleteAsync(playerRankingToDelete);
        //        Console.Write("Previous Ranking Deleted");
        //    }

        //    PlayerRanking playerRankingToAdd = new PlayerRanking();
        //    playerRankingToAdd.PlayerId = returnedPlayer.PlayerId;
        //    IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
        //    if (allPlayerRanks.Count() > 0)
        //    {
        //        var highestRankedPlayer = allPlayerRanks.Max(x => x.PlayerRank);
        //        playerRankingToAdd.PlayerRank = highestRankedPlayer + 1;
        //    }
        //    else
        //    {
        //        playerRankingToAdd.PlayerRank = 1;
        //    }

        //    playerRankingToAdd.TestUserProfileId = 2;
        //    playerRankingToAdd.isDefault = true;
        //    await _rankingRepository.AddAsync(playerRankingToAdd);
        //}

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var playerToEdit = await _playerRepository.GetByIdAsync(id);
            return View(playerToEdit);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player editedPlayer, IFormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return View(editedPlayer);
            }
            try
            {
                await _playerRepository.UpdateAsync(editedPlayer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedPlayer);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var playerToDelete = await _playerRepository.GetByIdAsync(id);
            return View(playerToDelete);
        }

        // POST: Player/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Player deletedPlayer, IFormCollection collection)
        {
            try
            {
                var playerToDelete = await _playerRepository.GetByIdAsync(id);
                await _playerRepository.DeleteAsync(playerToDelete);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var notDeletedPlayer = await _playerRepository.GetByIdAsync(id);
                return View(notDeletedPlayer);
            }
        }
    }
}