using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballManagerWebApp.Models
{
    public class PlayerRankingViewModel
    {
        public List<PlayerRankingModel> playerRankingModelList { get; set; }
        public string rankingScoreType { get; set; }
        public string playerPosition { get; set; }

    }
}
