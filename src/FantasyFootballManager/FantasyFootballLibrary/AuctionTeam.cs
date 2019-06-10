using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class AuctionTeam
    {
        public int AuctionTeamId { get; set; }
        public bool IsPractice { get; set; }
        public bool IsComplete { get; set; }
        public int TeamSize { get; set; }
        public int StartingBudget { get; set; }
        public int RemainingBudget { get; set; }
        public List<PlayerAuctioned> PlayersAuctioned { get; set; }

        //Navigation Properties - EF
        public int TestUserProfileId { get; set; }
        public TestUserProfile TestUserProfie { get; set; }
    }
}
