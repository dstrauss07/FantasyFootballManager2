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

          private readonly IAsyncRepository<TestUserProfile> _profileRepository;

        public ProfileUserController(IAsyncRepository<TestUserProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }


        // GET: api/ProfileUser/5
        [HttpGet("{requestedProfileId}", Name = "GetProfileId")]
        public async Task<IActionResult> Get(int requestedProfileId)
        {
            try
            {
                TestUserProfile profileToReturn = await _profileRepository.GetByIdAsync(requestedProfileId);
                return Ok(profileToReturn);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
