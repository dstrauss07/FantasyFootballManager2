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

        public ProfileUserController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
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

    }
}
