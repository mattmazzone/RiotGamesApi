using System;
using System.Net.Http;
using Newtonsoft.Json;


namespace LeagueApi
{
    class Program
    {
        static void Main()
        {
            using var client = new HttpClient();

            var api_key = "RGAPI-596c5b20-6a12-4ad4-ad84-eb65249ccfb7";


            var endpoint = new Uri("http://ddragon.leagueoflegends.com/cdn/12.12.1/data/en_US/champion.json");
            var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var currentUser = JsonConvert.DeserializeObject<User>(result, settings);

            if (currentUser == null)
            {
                Console.WriteLine("An error occured when parsing json and it returned null");
                return;
            }

            Console.WriteLine(currentUser.name);

            //string accountId = json.accountId;
            //Console.WriteLine(accountId);




        }
    }
}