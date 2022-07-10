using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            // Command to run in Cmd Prompt
            string command = "/C wmic PROCESS WHERE name='LeagueClientUx.exe' GET commandline";

            // Output string
            string output = "";

            // Declare a Process
            using (Process process = new Process())
            {
                // Process settings
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                
                // Start Process
                process.Start();

                // Read Output and store in string
                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                Console.WriteLine(output.Length);
                // Wait for exit
                process.WaitForExit();
            }
            // Regex rules
            Regex rx_port = new Regex(@"--app-port=([0-9]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex rx_pwd = new Regex(@"--remoting-auth-token=([\w-]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Match strings with regex rules
            string port_match = rx_port.Match(output).ToString();
            string pwd_match = rx_pwd.Match(output).ToString();

            // Extract values
            port_match = port_match.Replace("--app-port=", "");
            pwd_match = pwd_match.Replace("--remoting-auth-token=", "");


            Console.WriteLine("port: " + port_match);
            Console.WriteLine("pwd: " + pwd_match);


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
