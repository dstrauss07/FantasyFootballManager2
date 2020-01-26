using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FantasyFootballManagerWebApp.Models;
using StraussDa.FantasyFootballLibrary;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace FantasyFootballManagerWebApp.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Upload()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upload(IFormFile upload)
        //{
        //    Console.WriteLine("hello nurse");
        //    using (var reader = new StreamReader(upload.OpenReadStream()))
        //    {
        //        string currentLine;
        //        while ((currentLine = reader.ReadLine()) != null)
        //        {
        //            Console.WriteLine(currentLine);
        //            string[] columns = currentLine.Split(',');
        //            Console.WriteLine(columns);
        //            Player playerToAdd = new Player();
        //            playerToAdd.PlayerName = columns[0];
        //            playerToAdd.PlayerTeam = columns[1];
        //            string playerPos = Regex.Replace(columns[2],@"[\d-]",string.Empty);
        //            playerToAdd.PlayerPos = playerPos;
        //            Console.WriteLine(playerToAdd);

        //            await _playerRepository.AddNewPlayerAsync(newPlayer);
        //            var returnedPlayer = await _playerRepository.GetByPlayerByName(newPlayer.PlayerName);
        //            await AddPlayerRankingMethod.AddPlayerRanking(returnedPlayer, _rankingRepository, _playerRepository);

        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View();

        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
