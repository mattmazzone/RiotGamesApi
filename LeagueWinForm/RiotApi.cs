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

        private static string? client_port;
        private static string? client_pwd;
        private static string? client_username = "riot";

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
                

                // Error if League client is not open
                // TODO: Find better way / Warn user on UI 
                if (output.Length < 10)
                {
                    Console.WriteLine("League Client not detected");
                    return;
                }


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

            // Assign values
            client_port = port_match;
            client_pwd = pwd_match;
            
            return;
        }

        // Live Game 
        public static void GetAllGameData(string filename)
        {
            // Call private function to get port and password
            GetPortAndPwd();
            
            // Encode HTTP header in Base64
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(client_username + ":" + client_pwd));
            
            // Api url for request
            var apiRequest = "https://127.0.0.1:" + client_port + "/lol-champ-select/v1/session";


            // Create a HttpClientHandler
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            // Create client with handler and address
            HttpClient httpClient = new HttpClient(httpClientHandler) {
                BaseAddress = new Uri(apiRequest)
            };

            // Add encoded request header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            // Make the request
            var request = httpClient.GetAsync(apiRequest).Result;
          
            // Store Json as string 
            var content = request.Content.ReadAsStringAsync().Result.ToString();
            Console.WriteLine(content);


        }
    }
}
