using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using StraussDa.FantasyFootballLibrary.Interfaces;

namespace StraussDa.FantasyFootballLibrary.Infrastructure
{
    public class PlayerRepository : EfRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(PlayerDbContext playerDbContext): base(playerDbContext)
        {

        }

        public virtual async Task<Player> AddNewPlayerAsync(Player newPlayer)
        {

            var playerToCheck = await GetByPlayerByName(newPlayer.PlayerName);
            if(playerToCheck == null)
            {
                selectedRepository.Player.Add(newPlayer);
                await selectedRepository.SaveChangesAsync();
                return newPlayer;
            }
            else
            {
                return newPlayer;
            }

        }

        public virtual async Task<Player> GetByPlayerByName(string playerName)
        {
            try
            {
                var playerToReturn = await selectedRepository.Player.FirstAsync(x => x.PlayerName == playerName);

                return playerToReturn;
            }
            catch
            {
                return null;
            }

        }

        
    }
}
