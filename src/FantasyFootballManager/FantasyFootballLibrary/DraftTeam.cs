using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class DraftTeam : BaseEntity
    {
        public int DraftTeamId { get; set; }
        public bool IsPractice { get; set; }
        public bool IsCompleted { get; set; }
        public int TeamSize { get; set; }
        public string ScoringFormat { get; set; }
        public List<PlayerDrafted> PlayersDrafted { get; set; }

        //Navigation Properties - EF
        public int TestUserProfileId { get; set; }
        public TestUserProfile TestUserProfie { get; set; }
    }
}
