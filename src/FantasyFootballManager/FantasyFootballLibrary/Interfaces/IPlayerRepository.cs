using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StraussDa.FantasyFootballLibrary.Interfaces
{
    public interface IPlayerRepository : IAsyncRepository<Player>
    {
        Task <Player> GetByPlayerByName(string playerName);
    }
}
