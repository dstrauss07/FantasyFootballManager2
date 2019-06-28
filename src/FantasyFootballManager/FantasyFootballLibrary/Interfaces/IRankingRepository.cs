using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StraussDa.FantasyFootballLibrary.Interfaces
{
    public interface IRankingRepository : IAsyncRepository<PlayerRanking>
    {
        Task <PlayerRanking> GetByPlayerIdAsync(int id);
        Task<List<PlayerRanking>> SwapPlayerRanks(PlayerRanking playerRanking, int i, string scoring, string playerPosition);
        List<PlayerRanking> CreateListOfPlayersOfPosition(PlayerRanking playerToMove, IEnumerable<PlayerRanking> allPlayerRanks, IEnumerable<Player> allPlayers);
        Task MoveToTop(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove);
        Task MoveToBottom(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove);
    }
}
