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
        public ActionResult Index()
        {
            return View(_playerRepository.ListAllAsync());
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            return View(_playerRepository.GetByIdAsync(id));
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View(new Player());
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player newPlayer, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newPlayer);
                }

                _playerRepository.AddAsync(newPlayer);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(newPlayer);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_playerRepository.GetByIdAsync(id));
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Player editedPlayer, IFormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return View(editedPlayer);
            }
            try
            {
                _playerRepository.UpdateAsync(editedPlayer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedPlayer);
        }

// GET: Player/Delete/5
public ActionResult Delete(int id)
{
    return View(_playerRepository.GetByIdAsync(id));
}

// POST: Player/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Delete(int id, Player deletedPlayer, IFormCollection collection)
{
    try
    {
           
         _playerRepository.DeleteAsync(deletedPlayer);
        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}
    }
}