using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using FantasyFootballManagerWebApp.Models;
using FantasyFootballManagerWebApp.Methods;
using System.Reflection;



namespace FantasyFootballManagerWebApp.Controllers
{
    //MVC CONTROLLER
    public class PlayerRankingController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;
        private readonly CreatePlayerViewModels _createPlayerViewModels;

        public PlayerRankingController(IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
            _createPlayerViewModels = new CreatePlayerViewModels();
        }

        public async Task<IActionResult> Standard(string playerPosition)
        {
            ViewBag.scoringType = "Standard";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(playerPosition, _rankingRepository, _playerRepository);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository, _playerRepository);
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
            }
        }

        public async Task<IActionResult> Ppr(string playerPosition)
        {
            ViewBag.scoringType = "Ppr";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(playerPosition, _rankingRepository, _playerRepository);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PprRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository, _playerRepository);
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PprRank));
            }
        }

        public async Task<IActionResult> Dynasty(string playerPosition)
        {
            ViewBag.scoringType = "Dynasty";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(playerPosition, _rankingRepository, _playerRepository);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.DynastyRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository, _playerRepository);
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.DynastyRank));
            }
        }

        public async Task<IActionResult> MovePlayers(int id, string scoring, string playerPosition, int direction)
        {
            try
            {
                PlayerRanking playerRankingToChange = await _rankingRepository.GetByIdAsync(id);
                List<PlayerRanking> playerRankingList = await _rankingRepository.SwapPlayerRanks(playerRankingToChange, direction, scoring, playerPosition);
                Player playerToChange = await _playerRepository.GetByIdAsync(playerRankingList[0].PlayerId);
                Player otherPlayer = await _playerRepository.GetByIdAsync(playerRankingList[1].PlayerId);
                if (playerToChange.PlayerPos == otherPlayer.PlayerPos)
                {
                    await _rankingRepository.UpdatePosRanks(playerRankingList[0], playerRankingList[1], scoring, direction);
                }
                if (playerPosition == "All Players")
                {
                    return RedirectToAction(scoring);
                }
                else
                {
                    return RedirectToAction(scoring, new
                    {
                        playerPosition
                    });
                }
            }
            catch
            {
                //todo log exception
            }
            return RedirectToAction(scoring);
        }

        public async Task<IActionResult> MoveToTop(int id, string scoring, string playerPosition)
        {
            PlayerRanking playerToMove = await _rankingRepository.GetByIdAsync(id);
            IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
            IEnumerable<Player> allPlayers = await _playerRepository.ListAllAsync();
            List<PlayerRanking> playersOfPosition = _rankingRepository.CreateListOfPlayersOfPosition(playerToMove, allPlayerRanks, allPlayers);
            await _rankingRepository.MoveToTop(playerPosition, scoring, allPlayerRanks, playersOfPosition, playerToMove);
            if (playerPosition == "All Players")
            {
                return RedirectToAction(scoring);
            }
            if (playerPosition != "All Players")
            {
                return RedirectToAction(scoring, new
                {
                    playerPosition
                });
            }
            else
            {
                return RedirectToAction("Standard");
            }
        }

        public async Task<IActionResult> MoveToBottom(int id, string scoring, string playerPosition)
        {
            PlayerRanking playerToMove = await _rankingRepository.GetByIdAsync(id);
            IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();
            IEnumerable<Player> allPlayers = await _playerRepository.ListAllAsync();
            List<PlayerRanking> playersOfPosition = _rankingRepository.CreateListOfPlayersOfPosition(playerToMove, allPlayerRanks, allPlayers);
            await _rankingRepository.MoveToBottom(playerPosition, scoring, allPlayerRanks, playersOfPosition, playerToMove);
            if (playerPosition == "All Players")
            {
                return RedirectToAction(scoring);
            }
            if (playerPosition != "All Players")
            {
                return RedirectToAction(scoring, new
                {
                    playerPosition
                });
            }
            else
            {
                return RedirectToAction("Standard");
            }
        }

    }


}
