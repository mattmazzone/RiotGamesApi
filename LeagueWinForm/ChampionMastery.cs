using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWinForm
{
    public class ChampionMastery
    {
        // For Json Deserialize
        public string? ChampionID { get; set; }
        public string? ChampionLevel { get; set; }
        public string? ChampionPoints { get; set; }
        public string? LastPlayTime { get; set; }
        public string? ChampionPointsSinceLastLevel { get; set; }
        public string? ChampionPointsUntilNextLevel { get; set; }
        public string? ChestGranted { get; set; }
        public string? TokensEarned { get; set; }
        public string? SummonerId { get; set; }

        // Name of Champion using dictionnary
        public string? ChampionName { get; set; }
    }
}
