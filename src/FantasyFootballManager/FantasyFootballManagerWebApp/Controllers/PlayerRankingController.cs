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

        public async Task<IActionResult> Standard(string playerPosition)
        {
            ViewBag.scoringType = "Standard";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel(playerPosition);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
            }
        }

        public async Task<IActionResult> Ppr(string playerPosition)
        {
            ViewBag.scoringType = "Ppr";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel(playerPosition);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PprRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.PprRank));
            }
        }

        public async Task<IActionResult> Dynasty(string playerPosition)
        {
            ViewBag.scoringType = "Dynasty";
            if (playerPosition != null)
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel(playerPosition);
                ViewBag.playerPosition = playerPosition;
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.DynastyRank));
            }
            else
            {
                List<PlayerRankingModel> playerRankingModelList = await CreatePlayerViewModel();
                ViewBag.playerPosition = "All Players";
                return View(playerRankingModelList.OrderBy(p => p.playerRanking.DynastyRank));
            }
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



        private async Task<List<PlayerRankingModel>> CreatePlayerViewModel(string position)
        {
            IEnumerable<Player> allPlayers = await _playerRepository.ListAllAsync();
            List<PlayerRankingModel> playerRankingModelList = new List<PlayerRankingModel>();

            foreach (Player player in allPlayers.Where(p => p.PlayerPos == position))
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
                    await UpdatePosRanks(playerRankingList[0], playerRankingList[1], scoring, direction);
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

            List<PlayerRanking> playersOfPosition = new List<PlayerRanking>();
            Player origPlayer = await _playerRepository.GetByIdAsync(playerToMove.PlayerId);

            string posTocheck = origPlayer.PlayerPos;

            foreach (PlayerRanking p in allPlayerRanks)
            {
                Player playerToCheck = await _playerRepository.GetByIdAsync(p.PlayerId);

                if (playerToCheck.PlayerPos == posTocheck)
                {
                    playersOfPosition.Add(p);
                }
            }


            try
            {

                if (playerPosition == "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.PlayerRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PosRank);
                        foreach (PlayerRanking p in allPlayerRanks.Where(p => p.PlayerRank < playerToMove.PlayerRank).OrderBy(p => p.PlayerRank))
                        {
                            p.PlayerRank += 1;
                            if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                            {
                                p.PosRank += 1;
                            }
                            await _rankingRepository.UpdateAsync(p);
                        }
                        playerToMove.PlayerRank = highestRank;
                        playerToMove.PosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);

                    }
                    if (scoring == "Ppr")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.PprRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PprPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank < playerToMove.PprRank)
                            {
                                p.PprRank += 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.PprPosRank += 1;
                                }
                                await _rankingRepository.UpdateAsync(p);
                            }
                        }
                        playerToMove.PprRank = highestRank;
                        playerToMove.PprPosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);

                    }
                    if (scoring == "Dynasty")
                    {
                        int highestRank = allPlayerRanks.Min(x => x.DynastyRank);
                        int highestPosRank = playersOfPosition.Min(x => x.DynastyPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank < playerToMove.DynastyRank)
                            {
                                p.DynastyRank += 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.DynastyPosRank += 1;
                                }
                                await _rankingRepository.UpdateAsync(p);
                            }
                        }
                        playerToMove.DynastyRank = highestRank;
                        playerToMove.DynastyPosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);
                    }
                }
                if (playerPosition != "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int origPosRank = playerToMove.PosRank;
                        int origPlayerRank = playerToMove.PlayerRank;
                        int highestRank = playersOfPosition.Min(x => x.PlayerRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PosRank);
                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PosRank < origPosRank).OrderByDescending(p => p.PosRank))
                        {
                            int otherPlayerRank = p.PlayerRank;
                            int otherPosRank = p.PosRank;
                            p.PlayerRank = origPlayerRank;
                            p.PosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await _rankingRepository.UpdateAsync(p);
                        }

                        playerToMove.PlayerRank = highestRank;
                        playerToMove.PosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });
                    }
                    if (scoring == "Ppr")
                    {
                        int origPosRank = playerToMove.PprPosRank;
                        int origPlayerRank = playerToMove.PprRank;
                        int highestRank = playersOfPosition.Min(x => x.PprRank);
                        int highestPosRank = playersOfPosition.Min(x => x.PprPosRank);

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PprPosRank < origPosRank).OrderByDescending(p => p.PprPosRank))
                        {
                            int otherPlayerRank = p.PprRank;
                            int otherPosRank = p.PprPosRank;
                            p.PprRank = origPlayerRank;
                            p.PprPosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await _rankingRepository.UpdateAsync(p);
                        }

                        playerToMove.PprRank = highestRank;
                        playerToMove.PprPosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });

                    }
                    if (scoring == "Dynasty")
                    {
                        int origPosRank = playerToMove.DynastyPosRank;
                        int origPlayerRank = playerToMove.DynastyRank;
                        int highestRank = playersOfPosition.Min(x => x.DynastyRank);
                        int highestPosRank = playersOfPosition.Min(x => x.DynastyPosRank);

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.DynastyPosRank < origPosRank).OrderByDescending(p => p.DynastyPosRank))
                        {
                            int otherPlayerRank = p.DynastyRank;
                            int otherPosRank = p.DynastyPosRank;
                            p.DynastyRank = origPlayerRank;
                            p.DynastyPosRank = origPosRank;
                            origPlayerRank = otherPlayerRank;
                            origPosRank = otherPosRank;
                            await _rankingRepository.UpdateAsync(p);
                        }

                        playerToMove.DynastyRank = highestRank;
                        playerToMove.DynastyPosRank = highestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });
                    }
                }
            }
            catch
            {
                Console.WriteLine("failure in move to bottom");
                return RedirectToAction(scoring);
            }
            return RedirectToAction(scoring);
        }

        public async Task<IActionResult> MoveToBottom(int id, string scoring, string playerPosition)
        {
            PlayerRanking playerToMove = await _rankingRepository.GetByIdAsync(id);
            IEnumerable<PlayerRanking> allPlayerRanks = await _rankingRepository.ListAllAsync();

            List<PlayerRanking> playersOfPosition = new List<PlayerRanking>();

            Player origPlayer = await _playerRepository.GetByIdAsync(playerToMove.PlayerId);

            string posTocheck = origPlayer.PlayerPos;

            foreach (PlayerRanking p in allPlayerRanks)
            {
                Player playerToCheck = await _playerRepository.GetByIdAsync(p.PlayerId);

                if (playerToCheck.PlayerPos == posTocheck)
                {
                    playersOfPosition.Add(p);
                }
            }

            try
            {

                if (playerPosition == "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.PlayerRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.PosRank);
                        foreach (PlayerRanking p in allPlayerRanks.Where(p => p.PlayerRank > playerToMove.PlayerRank).OrderBy(p => p.PlayerRank))
                        {
                            p.PlayerRank -= 1;
                            if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                            {
                                p.PosRank -= 1;
                            }
                            await _rankingRepository.UpdateAsync(p);
                        }
                        playerToMove.PlayerRank = lowestRank;
                        playerToMove.PosRank = lowestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);

                    }
                    if (scoring == "Ppr")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.PprRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.PprPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank > playerToMove.PprRank)
                            {
                                p.PprRank -= 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.PprPosRank -= 1;
                                }
                                await _rankingRepository.UpdateAsync(p);
                            }
                        }
                        playerToMove.PprRank = lowestRank;
                        playerToMove.PprPosRank = lowestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);

                    }
                    if (scoring == "Dynasty")
                    {
                        int lowestRank = allPlayerRanks.Max(x => x.DynastyRank);
                        int lowestPosRank = playersOfPosition.Max(x => x.DynastyPosRank);
                        foreach (PlayerRanking p in allPlayerRanks)
                        {
                            if (p.PprRank > playerToMove.DynastyRank)
                            {
                                p.DynastyRank -= 1;
                                if (playersOfPosition.Any(ranking => ranking.PlayerRankingId == p.PlayerRankingId))
                                {
                                    p.DynastyPosRank -= 1;
                                }
                                await _rankingRepository.UpdateAsync(p);
                            }
                        }
                        playerToMove.DynastyRank = lowestRank;
                        playerToMove.DynastyPosRank = lowestPosRank;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring);
                    }
                }
                if (playerPosition != "All Players")
                {
                    if (scoring == "Standard")
                    {
                        int playersPosRank = playerToMove.PosRank;
                        int origPlayerRank = playerToMove.PlayerRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PosRank > playersPosRank).OrderBy(p => p.PosRank))
                        {
                            int otherPlayerRank = p.PlayerRank;
                            p.PlayerRank = origPlayerRank;
                            p.PosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await _rankingRepository.UpdateAsync(p);
                            posMovements++;
                        }

                        playerToMove.PlayerRank = origPlayerRank;
                        playerToMove.PosRank += posMovements;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });
                    }
                    if (scoring == "Ppr")
                    {
                        int playersPosRank = playerToMove.PprPosRank;
                        int origPlayerRank = playerToMove.PprRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.PprPosRank > playersPosRank).OrderBy(p => p.PprPosRank))
                        {
                            int otherPlayerRank = p.PprRank;
                            p.PprRank = origPlayerRank;
                            p.PprPosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await _rankingRepository.UpdateAsync(p);
                            posMovements++;
                        }

                        playerToMove.PprRank = origPlayerRank;
                        playerToMove.PprPosRank += posMovements;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });

                    }
                    if (scoring == "Dynasty")
                    {
                        int playersPosRank = playerToMove.DynastyPosRank;
                        int origPlayerRank = playerToMove.DynastyRank;
                        int posMovements = 0;

                        foreach (PlayerRanking p in playersOfPosition.Where(p => p.DynastyPosRank > playersPosRank).OrderBy(p => p.DynastyPosRank))
                        {
                            int otherPlayerRank = p.DynastyRank;
                            p.DynastyRank = origPlayerRank;
                            p.DynastyPosRank -= 1;
                            origPlayerRank = otherPlayerRank;
                            await _rankingRepository.UpdateAsync(p);
                            posMovements++;
                        }

                        playerToMove.DynastyRank = origPlayerRank;
                        playerToMove.DynastyPosRank += posMovements;
                        await _rankingRepository.UpdateAsync(playerToMove);
                        return RedirectToAction(scoring, new
                        {
                            playerPosition
                        });
                    }
                }
            }
            catch
            {
                Console.WriteLine("failure in move to bottom");
                return RedirectToAction(scoring);
            }
            return RedirectToAction(scoring);
        }


        private async Task UpdatePosRanks(PlayerRanking playerOne, PlayerRanking playerTwo, string scoring, int direction)
        {

            if (scoring == "Standard")
            {
                playerOne.PosRank -= direction;
                playerTwo.PosRank += direction;
            }
            if (scoring == "Ppr")
            {
                playerOne.PprPosRank -= direction;
                playerTwo.PprPosRank += direction;
            }
            if (scoring == "Dynasty")
            {
                playerOne.DynastyPosRank -= direction;
                playerTwo.DynastyPosRank += direction;
            }

            await _rankingRepository.UpdateAsync(playerOne);
            await _rankingRepository.UpdateAsync(playerTwo);
        }
    }


}
