using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using StraussDa.FantasyFootballLibrary.Infrastructure;


namespace FantasyFootballManagerWebApp.Controllers
{
    public class PlayerController : Controller
    {

        private readonly IAsyncRepository<Player> _playerRepository;

        public PlayerController(IAsyncRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
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

                await _playerRepository.AddAsync(newPlayer);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(newPlayer);
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