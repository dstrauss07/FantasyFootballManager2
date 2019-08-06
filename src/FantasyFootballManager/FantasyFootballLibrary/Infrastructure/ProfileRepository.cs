using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;
using System.Linq;

namespace StraussDa.FantasyFootballLibrary.Infrastructure
{
    public class ProfileRepository : EfRepository<TestUserProfile>, IProfileRepository
    {

        public ProfileRepository(PlayerDbContext playerDbContext) : base(playerDbContext)
        {

        }

        public async Task<TestUserProfile> GetProfileByEmailAsync(string email)
        {
            try
            {
                var profileToReturn = await selectedRepository.TestUserProfile.FirstAsync(x => x.UserEmail == email);
                return profileToReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Not Found: " + ex.Message);
                return null;
            }
        }

    }
}
