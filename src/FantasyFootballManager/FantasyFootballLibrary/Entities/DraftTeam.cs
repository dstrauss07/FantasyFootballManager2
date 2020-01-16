using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class DraftTeam : BaseEntity
    {
        public int DraftTeamId { get; set; }
        public bool IsCompleted { get; set; }
        public int TotalStartingQB { get; set; }
        public int TotalStartingRB { get; set; }
        public int TotalStartingWR { get; set; }
        public int TotalStartingTE { get; set; }
        public int TotalStartingD { get; set; }
        public int TotalStartingK { get; set; }
        public int TotalStartingFlex { get; set; }
        public int TotalStartingSFlex { get; set; }
        public int TotalPlayer { get; set; }
        public string ScoringFormat { get; set; }
        public string LeagueType { get; set;}
        public int LeagueSize { get; set; }
        public int DraftSlot { get; set; }
        public int StartingBudget { get; set; }

        public List<PlayerDrafted> AllPlayersDrafted { get; set; } = new List<PlayerDrafted>();
        public List<PlayerDrafted> MyPlayersDrafted { get; set; } = new List<PlayerDrafted>();

        //Navigation Properties - EF
        public int TestUserProfileId { get; set; }
        public TestUserProfile TestUserProfie { get; set; }
    }
}

