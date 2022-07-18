using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueWinForm.Forms
{
    public partial class champ_select : Form
    {
        // Use Hash Set because list should be unique
        private static HashSet<string> bannedChampions = new HashSet<string>();
        private static int numBans = 0;

        private static string champ_select_phase = "";

        private static bool inGame = false;
        private static bool changeBanLabel = false;
        private static bool changePlayerLabels = false;

        public static bool checkPhase = true;


        // Instance of login page
        public static champ_select? instance;

        public champ_select()
        {
            InitializeComponent();
            instance = this;

        }

        // Set the banned champions in the hash set
        public static void GetChampSelectBans()
        {
            // Check if ban label has already been updated
            if (changeBanLabel is true)
            {
                return;
            }

            // Check if all bans are done
            if (numBans == 10)
            {
                string labelText = "";

                // Prepare string for UI label
                foreach (string name in bannedChampions)
                {
                    // Condition for last element comma
                    if (name == bannedChampions.ElementAt(bannedChampions.Count - 1))
                    {
                        labelText = labelText + name;
                    }
                    else
                    {
                        labelText = labelText + name + ", ";
                    }

                }
                // Change Banned champ label in UI
                if (instance is not null)
                {
                    instance.BanListValues.Text = labelText;
                    changeBanLabel = true;
                }
                return;
            }

            // Reset banned champ counter
            numBans = 0;

            // Do a Riot API Call
            var json = RiotApi.GetChampSelectSession();

            // Parse json file
            JObject jsonObj = JObject.Parse(json);

            // ["actions"][0] returns the list of 10 actions (bans)
            JToken? jsonToken = jsonObj["actions"]?[0];
            if (jsonToken is null)
            {
                Console.WriteLine("Json token is null in GetChampSelectBans()");
                return;
            }
            string result = jsonToken.ToString();
            List<ChampionBan>? tempList = new List<ChampionBan>();
            tempList = JsonConvert.DeserializeObject<List<ChampionBan>>(result);

            // Check if successful Deserialize
            if (tempList is null)
            {
                return;
            }

            // Find Id in dictionary and add champion name in HashSet 
            foreach (ChampionBan ban in tempList)
            {
                if (ban.ChampionId is not null && ban.Completed is not null && RiotApi.championList is not null)
                {
                    if (ban.Completed == "true")
                    {
                        numBans++;
                        int key = Int32.Parse(ban.ChampionId);

                        if (RiotApi.championList.TryGetValue(key, out string? value))
                        {
                            ban.ChampionId = value;
                        }
                        bannedChampions.Add(ban.ChampionId);
                    }
                }
            }
        }

        // Set the player name and the champ they're playing
        // TODO: Ranks and Win rate?
        public static void GetChampSelectPlayer()
        {
            // Check if ban label has already been updated
            if (changePlayerLabels is true)
            {
                return;
            }


            // Do a Riot API Call
            var json = RiotApi.GetChampSelectSession();

            // Parse json file
            JObject jsonObj = JObject.Parse(json);

            // ["actions"][0] returns the list of 10 actions (bans)
            JToken? jsonToken = jsonObj["myTeam"];
            if (jsonToken is null)
            {
                Console.WriteLine("Json token is null in GetChampSelectPlayer()");
                return;
            }
            string result = jsonToken.ToString();

            List<TeamPlayer>? tempList = new List<TeamPlayer>();
            tempList = JsonConvert.DeserializeObject<List<TeamPlayer>>(result);

            // Check if successful Deserialize
            if (tempList is null)
            {
                return;
            }

            // Find Id in dictionary and add champion name in HashSet 
            foreach (TeamPlayer ban in tempList)
            {
                if (ban.ChampionId is not null && RiotApi.championList is not null)
                {
                    int key = Int32.Parse(ban.ChampionId);

                    if (RiotApi.championList.TryGetValue(key, out string? value))
                    {
                        ban.ChampionId = value;
                    }
                    bannedChampions.Add(ban.ChampionId);
                }

            }

            // Set Labels in UI
            for (int i = 0; i < tempList.Count; i++)
            {
                string labelName = "MyTeam" + i + "Champ";
                if (instance is not null)
                {
                    var control = instance.Controls.Find(labelName, true).FirstOrDefault();
                    if (control is not null)
                    {
                        control.Text = tempList[i].ChampionId;
                    }
                    
                }
            }

            changePlayerLabels = true;
        }


        public async static void GetChampSelectPhase()
        {
            string currentPhase = "";
            string oldPhase = "";


            while (checkPhase)
            {
                currentPhase = RiotApi.GetChampSelectPhase();

                switch (currentPhase)
                {
                    case "PLANNING":
                        champ_select_phase = "PLANNING";
                        GetChampSelectPlayer();
                        break;

                    case "BAN_PICK":
                        champ_select_phase = "BAN_PICK";
                        GetChampSelectBans();
                        break;

                    case "FINALIZATION":
                        champ_select_phase = "FINALIZATION";

                        break;

                    case "GAME_STARTING":
                        champ_select_phase = "GAME_STARTING";
                        inGame = true;
                        break;

                    case "NOT_IN_CS":
                        champ_select_phase = "NOT_IN_CS";
                        break;

                    default:
                        champ_select_phase = "CS_OVER";
                        break;
                }

                if (oldPhase != currentPhase)
                {
                    Console.WriteLine("Checking Champ Select Phase :" + champ_select_phase);
                }
                oldPhase = currentPhase;

                await Task.Delay(1000);


                if (inGame == true)
                {
                    Console.WriteLine("YIELD");
                    inGame = false;

                    return;
                }
            }
        }


        private class ChampionBan
        {
            public string? ActorCellID { get; set; }
            public string? ChampionId { get; set; }
            public string? Completed { get; set; }
            public string? Id { get; set; }
            public string? IsAllyAction { get; set; }
            public string? IsInProgress { get; set; }
            public string? Type { get; set; }

        }

        private class TeamPlayer
        {
            public string? AssignedPosition { get; set; }
            public string? CellIs { get; set; }
            public string? ChampionId { set; get; }
            public string? ChampionPickIntent { get; set; }
            public string? EntitledFeatureType { get; set; }
            public string? SelectedSkinId { get; set; }
            public string? Spell1Id { set; get; }
            public string? Spell2Id { set; get; }
            public string? SummonerId { set; get; }
            public string? Team { get; set; }
            public string? WardSkinId { set; get; }

        }




    }










}
