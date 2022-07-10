using Newtonsoft.Json;

namespace LeagueWinForm.Forms
{
    public partial class champion_mastery : Form
    {
        private User? aUser;
        public static champion_mastery? instance;

        public champion_mastery(User user)
        {
            this.aUser = user;
            instance = this;

            InitializeComponent();
            OnPageLoad();


        }

        public champion_mastery(string context)
        {

        }

        private void OnPageLoad()
        {
            using var client = new HttpClient();

            // Check for null user
            if (aUser is null || aUser.Id is null)
            {
                return;
            }

            // Get Api Call
            var endpoint = RiotApi.GetChampionMasteriesById(aUser.Id);

            // Send API Call
            var request = client.GetAsync(endpoint).Result;

            // Check HTTP status code 
            var statusCode = request.StatusCode.ToString();
            if (statusCode != "OK")
            {
                // TODO : Add error label
                return;
            }

            // Store Json 
            var content = request.Content.ReadAsStringAsync().Result;

            // Json settings ignore Null values because we check right after
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            // Deserialize Json into a user object
            var listChampionMasteries = JsonConvert.DeserializeObject<List<ChampionMastery>>(content, settings);

            // Check if user object is null
            if (listChampionMasteries is null)
            {
                return;
            }
            if (RiotApi.championList is null)
            {
                return;
            }

            // Assigns champion name using dictionary
            foreach (ChampionMastery championMastery in listChampionMasteries)
            {
                if (championMastery.ChampionID is null)
                {
                    break;
                }

                int key = Int32.Parse(championMastery.ChampionID);


                if (RiotApi.championList.TryGetValue(Int32.Parse(championMastery.ChampionID), out string? value))
                {
                    championMastery.ChampionName = value;
                }


            }

            champion1Value.Text = listChampionMasteries[0].ChampionName;
            champion2Value.Text = listChampionMasteries[1].ChampionName;
            champion3Value.Text = listChampionMasteries[2].ChampionName;


        }



    }
}
