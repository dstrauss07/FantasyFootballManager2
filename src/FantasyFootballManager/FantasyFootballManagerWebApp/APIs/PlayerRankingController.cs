using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraussDa.FantasyFootballLibrary.Interfaces;
using StraussDa.FantasyFootballLibrary;
using FantasyFootballManagerWebApp.Models;
using FantasyFootballManagerWebApp.Methods;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace FantasyFootballManagerWebApp.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerRankingController : ControllerBase
    {

        public IConfiguration Configuration { get; set; }
        public int defaultProfileId = 2025;
                       private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;
        private readonly CreatePlayerViewModels _createPlayerViewModels;

        public PlayerRankingController(IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
            _createPlayerViewModels = new CreatePlayerViewModels(_rankingRepository);
        }

        // GET: api/PlayerRankingApi
        [HttpGet]
        public async Task<IActionResult> GetPlayerRankings()
        {
            List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository,_playerRepository);
            return Ok(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
        }

        // GET: api/PlayerRankingApi/5
        [HttpGet("{requestedProfileId}", Name = "Get")]
        public async Task<IActionResult> GetPlayerRankings(int requestedProfileId)
        {
            try
            {
                List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository, _playerRepository, requestedProfileId);




                if (playerRankingModelList.Count == 0||playerRankingModelList == null)
                {
                   playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository, _playerRepository, defaultProfileId);
                }
                return Ok(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
            }
            catch
            {
                //TODO ADD LOGGING
                    return null;
            }
          }

        // POST: api/PlayerRankingApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PlayerRankingApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPatch()]

        public async Task<IActionResult> patchPlayerRankings([FromBody] List<PlayerRankingModel> playerRankingsToUpdate)
        {
            for (int i = 0; i < playerRankingsToUpdate.Count; i++)
            {
                await _rankingRepository.UpdateAsync(playerRankingsToUpdate[i].playerRanking);
            }

            //PlayerRanking playerRankingToUpdate = JsonConvert.DeserializeObject<PlayerRanking>(playerRankingToUpdateString);
   
            return null;
      
         }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
