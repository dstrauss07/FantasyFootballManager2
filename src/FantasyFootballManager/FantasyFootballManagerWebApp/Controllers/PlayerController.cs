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
using System.IO;
using System.Text.RegularExpressions;

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

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile upload)
        {
            using (var reader = new StreamReader(upload.OpenReadStream()))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine(currentLine);
                    string[] columns = currentLine.Split(',');
                    Console.WriteLine(columns);
                    Player playerToAdd = new Player();
                    playerToAdd.PlayerName = columns[0];
                    playerToAdd.PlayerTeam = columns[1];
                    string playerPos = Regex.Replace(columns[2], @"[\d-]", string.Empty);
                    playerToAdd.PlayerPos = playerPos;
                    Console.WriteLine(playerToAdd);
                    await _playerRepository.AddNewPlayerAsync(playerToAdd);
                    var returnedPlayer = await _playerRepository.GetByPlayerByName(playerToAdd.PlayerName);
                    await AddPlayerRankingMethod.AddPlayerRanking(returnedPlayer, _rankingRepository, _playerRepository);
                }
            }

            return RedirectToAction(nameof(Index));

        }



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