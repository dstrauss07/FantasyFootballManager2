using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerDrafted : BaseEntity
    {

        public int PlayerDraftedId { get; set; }

        public int PickNumber { get; set; }
        public int TeamPicked { get; set; }
        public int RoundPicked { get; set; }
        public int WinningBid { get; set; }
                     
        //Navigation Properties - EF
        public int PlayerRankingId { get; set; }
        public Player PlayerRanking { get; set; }

    }
}
