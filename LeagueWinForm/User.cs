using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWinForm
{
    public class User
    {
        // Private fields are auto-generated
        public string? Id { get; set; }
        public string? AccountId { get; set; }
        public string? Puuid { get; set; }
        public string? Name { get; set; }
        public string? ProfileIconId { get; set; }
        public string? RevisionDate { get; set; }
        public string? SummonerLevel { get; set; }
        public User(string context)
        {
            Id =  context;
            AccountId = context;
            Puuid = context;
            Name = context;
            ProfileIconId = context;
            RevisionDate = context;
            SummonerLevel = context;
        }
        public User()
        {

        }


        public bool CheckForNull()
        {
            if (Id is null || AccountId is null || Puuid is null || Name is null || ProfileIconId is null || RevisionDate is null || SummonerLevel is null)
            {
                return true;
            }
            return false;
        }

        

    }
}
