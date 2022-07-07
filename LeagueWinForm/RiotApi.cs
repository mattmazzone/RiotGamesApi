using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueWinForm
{
    internal class RiotApi
    {
        public static Dictionary<int, string>? championList;

        public static void LoadChampionDictionnary()
        {
            using (StreamReader r = new("..\\..\\..\\LeagueData\\championFull.json"))
            {
                string json = r.ReadToEnd();
                JObject jsonObj = JObject.Parse(json);

                // TODO: Fix possible null reference
                string results = jsonObj["keys"].ToString();
                championList = JsonConvert.DeserializeObject<Dictionary<int, string>>(results);
            }


            /* 
             * Print dictionnary
            foreach (KeyValuePair<int, string> champion in championList)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                champion.Key, champion.Value);
            }
            */
        }


        public static Uri GetSummonerByName(string apiKey, string summonerName, string region)
        {
            var apiRequest = new Uri("https://" + region + ".api.riotgames.com/lol/summoner/v4/summoners/by-name/" + summonerName + "?api_key=" + apiKey);
            return apiRequest;
        }
    }
}
