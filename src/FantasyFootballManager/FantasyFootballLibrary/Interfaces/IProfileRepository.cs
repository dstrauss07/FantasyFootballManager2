using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StraussDa.FantasyFootballLibrary.Interfaces
{
    public interface IProfileRepository : IAsyncRepository<TestUserProfile>
    {
        Task<TestUserProfile> GetProfileByEmailAsync(string email);
    }
}
