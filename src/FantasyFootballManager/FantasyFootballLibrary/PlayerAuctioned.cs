using System;
using System.Collections.Generic;
using System.Text;

namespace StraussDa.FantasyFootballLibrary
{
    public class PlayerAuctioned
    {
        public int PlayerAuctionedId { get; set; }
        public int WinningBid { get; set; }


        //navigation properties for EF
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int AuctionTeamId { get; set; }
        public AuctionTeam AuctionTeam { get; set; }
    }
}
