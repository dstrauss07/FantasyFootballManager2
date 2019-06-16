using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerRanking: BaseEntity
    {
        public int PlayerRankingId { get; set; }
        public bool IsDefault { get; set; }
        public int PlayerRank { get; set; }
        public int PosRank { get; set; }
        public decimal PlayerValue { get; set; }
        public int PprRank { get; set; }
        public int PprPosRank { get; set; }
        public decimal PprValue { get; set; }
        public int SflexRank { get; set; }
        public decimal SflexValue { get; set; }
        public int PlayerRiskLevel { get; set; }

        //Navigation Properties - EF
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int TestUserProfileId { get; set; }
        public TestUserProfile TestUserProfie { get; set; }
    }
}
