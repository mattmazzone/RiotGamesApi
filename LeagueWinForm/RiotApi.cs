using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueWinForm
{
    internal class RiotApi
    {
        private string apiKey; 
        private HttpClient httpClient;



        public RiotApi(string apiKey)
        {
            this.apiKey = apiKey;
            httpClient = new HttpClient();

        } 



    }
}
