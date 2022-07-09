using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace LeagueWinForm
{
    internal class RiotApi
    {
        public static Dictionary<int, string>? championList;

        private static string? apiKey;
        private static string? region;

        public static void LoadChampionDictionnary()
        {
            using (StreamReader r = new("..\\..\\..\\LeagueData\\championFull.json"))
            {
                // Read and parse json file
                string json = r.ReadToEnd();
                JObject jsonObj = JObject.Parse(json);

                JToken? jsonToken = jsonObj["keys"];
                if (jsonToken is null)
                {
                    Console.WriteLine("Json token is null in LoadChampionDictionnary()");
                    return;
                }
                // Convert to string
                string results = jsonToken.ToString();

                // Deserialize into a dictionary
                championList = JsonConvert.DeserializeObject<Dictionary<int, string>>(results);
            }

        }
        public static void PrintChampionListDictionary()
        {
            if (championList is null)
            {
                return;
            }
            //Print dictionnary
            foreach (KeyValuePair<int, string> champion in championList)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                champion.Key, champion.Value);
            }
        }


        public static Uri GetSummonerByName(string apiKey, string summonerName, string region)
        {
            var apiRequest = new Uri("https://" + region + ".api.riotgames.com/lol/summoner/v4/summoners/by-name/" + summonerName + "?api_key=" + apiKey);
            RiotApi.apiKey = apiKey;
            RiotApi.region = region;
            return apiRequest;
        }

        public static Uri GetChampionMasteriesById(string id)
        {
            var apiRequest = new Uri("https://" + RiotApi.region + ".api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/" + id + "?api_key=" + RiotApi.apiKey);
            return apiRequest;
        }

        // WMIC Commands
        public static void GetPortAndPwd()
        {
            string command = "/C wmic PROCESS WHERE name='LeagueClientUx.exe' GET commandline";


            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                StreamReader reader = process.StandardOutput;
                string output = reader.ReadToEnd();

                Console.WriteLine(output);

                process.WaitForExit();
            }
            
            
        }
        // Live Game 
        public static void GetAllGameData()
        {


            var username = "riot";
            var password = "mKJ_86OnEwotIOwKcP4KSA";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));
            

            var apiRequest = "https://127.0.0.1:56725/lol-champ-select/v1/session/";
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            HttpClient httpClient = new HttpClient(httpClientHandler) {
                BaseAddress = new Uri(apiRequest)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            var request = httpClient.GetAsync(apiRequest).Result;
          
            // Store Json 
            var content = request.Content.ReadAsStringAsync().Result.ToString();
            Console.WriteLine(content);
        }
    }
}
