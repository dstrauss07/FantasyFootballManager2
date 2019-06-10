using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerDrafted : BaseEntity
    {
        public int PlayerDraftedId { get; set; }
        public int DraftedRound { get; set; }
        public int RoundPick { get; set; }
        public int OverallPick { get; set; }

        //navigation properties for EF
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int DraftTeamId { get; set; }
        public DraftTeam DraftTeam { get; set; }
    }
}
