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

namespace FantasyFootballManagerWebApp.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerRankingController : ControllerBase
    {

        public IConfiguration Configuration { get; set; }


        private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;
        private readonly CreatePlayerViewModels _createPlayerViewModels;

        public PlayerRankingController(IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
            _createPlayerViewModels = new CreatePlayerViewModels();
        }

        // GET: api/PlayerRankingApi
        [HttpGet]
        public async Task<IActionResult> GetPlayerRankings()
        {
            List<PlayerRankingModel> playerRankingModelList = await _createPlayerViewModels.CreatePlayerViewModel(_rankingRepository,_playerRepository);
            return Ok(playerRankingModelList.OrderBy(p => p.playerRanking.PlayerRank));
        }

        // GET: api/PlayerRankingApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
