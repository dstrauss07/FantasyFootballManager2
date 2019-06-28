using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StraussDa.FantasyFootballLibrary.Interfaces
{
    public interface IRankingRepository : IAsyncRepository<PlayerRanking>
    {
        Task <PlayerRanking> GetByPlayerIdAsync(int id);
        Task<PlayerRanking> GetByPlayerRankAsync(int rank);
        Task<PlayerRanking> GetByPlayerPprRankAsync(int rank);
        Task<PlayerRanking> GetByPlayerDynastyRankAsync(int rank);
        Task<PlayerRanking> GetByPlayerStandardPosRank(int rank);
        Task<PlayerRanking> GetByPlayerPprPosRank(int rank);
        Task<PlayerRanking> GetByPlayerDynastyPosRank(int rank);
        List<PlayerRanking> CreateListOfPlayersOfPosition(PlayerRanking playerToMove, IEnumerable<PlayerRanking> allPlayerRanks, IEnumerable<Player> allPlayers);
        Task<List<PlayerRanking>> SwapPlayerRanks(PlayerRanking playerRanking, int i, string scoring, string playerPosition);
        Task UpdatePosRanks(PlayerRanking playerOne, PlayerRanking playerTwo, string scoring, int direction);
        Task MoveToTop(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove);
        Task MoveToBottom(string playerPosition, string scoring, IEnumerable<PlayerRanking> allPlayerRanks, List<PlayerRanking> playersOfPosition, PlayerRanking playerToMove);
    }
}
