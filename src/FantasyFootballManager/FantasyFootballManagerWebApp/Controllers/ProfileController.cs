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
    public class ProfileController : Controller
    {

        private readonly IAsyncRepository<TestUserProfile> _profileRepository;

        public ProfileController(IAsyncRepository<TestUserProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            var listOfProfiles = await _profileRepository.ListAllAsync();
            return View(listOfProfiles);
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var profileDetails = await _profileRepository.GetByIdAsync(id);
            return View(profileDetails);
        }

        // GET: Player/Create
        public ActionResult Create()
        {

            return View(new TestUserProfile());
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestUserProfile newProfile, IFormCollection collection)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newProfile);
                }

                await _profileRepository.AddAsync(newProfile);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(newProfile);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var profileToEdit = await _profileRepository.GetByIdAsync(id);
            return View(profileToEdit);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestUserProfile editedProfile, IFormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return View(editedProfile);
            }
            try
            {
                await _profileRepository.UpdateAsync(editedProfile);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedProfile);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var profileToDelete = await _profileRepository.GetByIdAsync(id);
            return View(profileToDelete);
        }

        // POST: Player/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, TestUserProfile deletedProfile, IFormCollection collection)
        {
            try
            {
                var profileToDelete = await _profileRepository.GetByIdAsync(id);
                await _profileRepository.DeleteAsync(profileToDelete);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var notDeletedPlayer = await _profileRepository.GetByIdAsync(id);
                return View(notDeletedPlayer);
            }
        }
    }
}