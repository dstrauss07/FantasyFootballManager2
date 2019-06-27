using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class Player : BaseEntity
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPos { get; set; }
        public string PlayerTeam { get; set; }

        public static implicit operator Player(PlayerRanking v)
        {
            throw new NotImplementedException();
        }
    }
}
