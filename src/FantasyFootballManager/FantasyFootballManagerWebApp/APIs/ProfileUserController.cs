using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraussDa.FantasyFootballLibrary;
using StraussDa.FantasyFootballLibrary.Interfaces;

namespace FantasyFootballManagerWebApp.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUserController : ControllerBase
    {

        private readonly IProfileRepository _profileRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRankingRepository _rankingRepository;

        public ProfileUserController(IProfileRepository profileRepository, IPlayerRepository playerRepository, IRankingRepository rankingRepository)
        {
            _profileRepository = profileRepository;
            _playerRepository = playerRepository;
            _rankingRepository = rankingRepository;
        }


        // GET: api/ProfileUser/5
        //[HttpGet("{requestedProfileId}", Name = "GetProfileId")]
        //public async Task<IActionResult> Get(int requestedProfileId)
        //{
        //    try
        //    {
        //        TestUserProfile profileToReturn = await _profileRepository.GetByIdAsync(requestedProfileId);
        //        return Ok(profileToReturn);
        //    }
        //    catch(Exception ex)
        //    {

        //        return null;
        //    }
        //}

        [HttpGet("{requestedProfileEmail}", Name = "GetProfileByEmail")]
        public async Task<IActionResult> Get(string requestedProfileEmail)
        {
            try
            {
                TestUserProfile profileToReturn = await _profileRepository.GetProfileByEmailAsync(requestedProfileEmail);
                return Ok(profileToReturn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Not Found");
                return null;
            }
        }
        //        ,{emailToPass
        //    },{passwordToPass
        //}


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dynamic data)
        {
            try
            {
                TestUserProfile profileToReturn = await _profileRepository.GetProfileByEmailAsync(data.email);
                return Ok(profileToReturn);
            }
            catch
            {
                try
                {
                    TestUserProfile userProfileToAdd = new TestUserProfile();
                    userProfileToAdd.Name = data.name;
                    userProfileToAdd.UserEmail = data.email;
                    userProfileToAdd.UserPassword = data.password;
                    await _profileRepository.AddAsync(userProfileToAdd);
                    TestUserProfile profileToReturn = await _profileRepository.GetProfileByEmailAsync(userProfileToAdd.UserEmail);
                    await _rankingRepository.CreateListOfPlayerRanksByProfileId(profileToReturn.TestUserProfileId, _playerRepository, _rankingRepository);
                    return Ok(profileToReturn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }
        }
    }
}
