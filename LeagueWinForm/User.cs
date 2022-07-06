using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWinForm
{
    public class User
    {

        // 
        public User(string context)
        {
            id =  context;
            accountId = context;
            puuid = context;
            name = context;
            profileIconId = context;
            revisionDate = context;
            summonerLevel = context;
        }

        public string? id { get; set; }
        public string? accountId { get; set; }
        public string? puuid { get; set; }
        public string? name { get; set; }
        public string? profileIconId { get; set; }
        public string? revisionDate { get; set; }
        public string? summonerLevel { get; set; }

    }
}
