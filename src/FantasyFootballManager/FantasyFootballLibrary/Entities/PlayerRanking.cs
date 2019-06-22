using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerRanking: BaseEntity
    {
        public int PlayerRankingId { get; set; }
        public bool isDefault { get; set; }
        public int PlayerRank { get; set; }
        public int PosRank { get; set; }
        public int PprRank { get; set; }
        public int PprPosRank { get; set; }
        public int DynastyRank { get; set; }
        public int DynastyPosRank { get; set; }

        //Navigation Properties - EF
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int TestUserProfileId { get; set; }
        public TestUserProfile TestUserProfie { get; set; }
    }
}
