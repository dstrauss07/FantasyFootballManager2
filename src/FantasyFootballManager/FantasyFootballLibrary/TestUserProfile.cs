using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class TestUserProfile
    {
        public int TestUserProfileId { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }

        //public List<AuctionTeam> AuctionTeams { get; set; }
        public List<DraftTeam> DraftedTeams { get; set; }
        //public List<PlayerRanking> MyPlayerRanks { get; set; }
    }
}
