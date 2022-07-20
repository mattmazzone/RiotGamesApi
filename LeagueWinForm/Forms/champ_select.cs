using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueWinForm.Forms
{
    public partial class champ_select : Form
    {
        // Use Hash Set because list should be unique
        private static HashSet<string> bannedChampions = new HashSet<string>();


        private static string champ_select_phase = "";

        private static bool inGame = false;
        private static bool changeBanLabel = false;
        private static bool changePlayerAndChampLabels = false;

        public static bool checkPhase = true;


        // Instance of login page
        public static champ_select? instance;

        public champ_select()
        {
            InitializeComponent();
            instance = this;

        }

        // Set the banned champions in the hash set
        public static async void GetChampSelectBans()
        {
            Console.WriteLine("checking bans");
            // Check if ban label has already been updated
            int numBans = 0;
            if (changeBanLabel is true)
            {
                
                return;
            }

            // Check if all bans are done
            string labelText = "";

            // Prepare string for UI label
            foreach (string name in bannedChampions)
            {
                //Console.WriteLine(name);
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
                if (instance.BanListValues.InvokeRequired)
                {
                    instance.BanListValues.Invoke(new Action(() => instance.BanListValues.Text = labelText));
                    //changeBanLabel = true;
                }
            }
            


            // Reset banned champ counter
            numBans = 0;

            // Do a Riot API Call
            var json = await RiotApi.GetChampSelectSession();

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
            Console.WriteLine(result);

            List<ChampionBan>? tempList = new List<ChampionBan>();
            tempList = JsonConvert.DeserializeObject<List<ChampionBan>>(result);

            // Check if successful Deserialize
            if (tempList is null)
            {
                Console.WriteLine("Templist null");
                return;
            }
            Console.WriteLine("yo");
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
                            bannedChampions.Add(ban.ChampionId);
                            Console.WriteLine(ban.ChampionId);
                        }

                        
                    }
                }
            }



        }

        // Set the player name and the champ they're playing
        // TODO: Ranks and Win rate?
        public static async void GetChampSelectPlayer()
        {
            // Check if player label has already been updated
            int championCounter = 0;
            if (changePlayerAndChampLabels is true)
            {
                return;
            }

            // Do a Riot API Call
            var json = await RiotApi.GetChampSelectSession();

            if (json == string.Empty)
            {
                return;
            }

            // Parse json file
            JObject jsonObj = JObject.Parse(json);

            // returns myteam
            JToken? jsonToken = jsonObj["myTeam"];
            if (jsonToken is null)
            {
                Console.WriteLine("Json token is null in GetChampSelectPlayer()");
                return;
            }
            string result = jsonToken.ToString();

            // List of Players
            List<TeamPlayer>? tempList = new List<TeamPlayer>();
            tempList = JsonConvert.DeserializeObject<List<TeamPlayer>>(result);

            // Check if successful Deserialize
            if (tempList is null)
            {
                return;
            }

            // Find Id in dictionary and add champion name in HashSet 
            foreach (TeamPlayer champion in tempList)
            {
                if (champion.ChampionId is not null && RiotApi.championList is not null)
                {
                    int key = Int32.Parse(champion.ChampionId);

                    if (RiotApi.championList.TryGetValue(key, out string? value))
                    {
                        champion.ChampionId = value;
                        championCounter++;
                    }
                }
            }

            // Store players in match in a list 
            List<LCUPlayerInfo> myTeamUsers = new List<LCUPlayerInfo>();
            foreach (TeamPlayer player in tempList)
            {
                if (player.SummonerId is not null)
                {
                    var user = JsonConvert.DeserializeObject<LCUPlayerInfo>(RiotApi.GetSummonerById(player.SummonerId).Result);
                    if (user is not null)
                    {
                        myTeamUsers.Add(user);
                    }
                }
            }


            // Set Labels in UI
            // An invoke is required because it is running on a different thread
            for (int i = 1; i < tempList.Count + 1; i++)
            {
                // Concat label name
                string labelChamp = "MyTeam" + i + "Champ";
                string labelPlayer = "MyTeam" + i + "Name";

                if (instance is not null)
                {
                    // Find labels in UI
                    var champName = instance.Controls.Find(labelChamp, true).FirstOrDefault();
                    var playerName = instance.Controls.Find(labelPlayer, true).FirstOrDefault();

                    if (champName is not null)
                    {
                        if (champName.InvokeRequired)
                        {
                            champName.Invoke(new Action(() => champName.Text = tempList[i - 1].ChampionId));
                        }

                    }

                    if (playerName is not null)
                    {
                        if (playerName.InvokeRequired)
                        {
                            playerName.Invoke(new Action(() => playerName.Text = myTeamUsers[i - 1].DisplayName));
                        }
                    }
                }
            }

            if (championCounter == 5)
            {
                changePlayerAndChampLabels = true;
            }
        }

        public static async void GetGameMode()
        {
            var json = await RiotApi.GetLobbyGameMode();

            if (json == string.Empty)
            {
                return;
            }

            // Parse json file
            JObject jsonObj = JObject.Parse(json);

            // Get Game Mode from json
            JToken? jsonToken = jsonObj["gameConfig"]?["gameMode"];
            if (jsonToken is null)
            {
                Console.WriteLine("Json token is null in GetGameMode()");
                return;
            }
            string gameType = jsonToken.ToString();

            // Set Labels in UI
            if (gameType is not null && instance is not null)
            {
                if (instance.GameModeValue.InvokeRequired)
                {
                    instance.GameModeValue.Invoke(new Action(() => instance.GameModeValue.Text = gameType));
                }
            }

        }


        public async static void GetChampSelectPhase()
        {
            string currentPhase = "";
            string oldPhase = "";

            while (checkPhase)
            {
                currentPhase = await RiotApi.GetChampSelectPhase();
                switch (currentPhase)
                {
                    case "PLANNING":
                        champ_select_phase = "PLANNING";

                        break;

                    case "BAN_PICK":
                        champ_select_phase = "BAN_PICK";
                        GetGameMode();
                        GetChampSelectPlayer();
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
                    if (instance is not null)
                    {

                        if (instance.PhaseValue.InvokeRequired)
                        {
                            instance.PhaseValue.Invoke(new Action(() => instance.PhaseValue.Text = champ_select_phase));
                        }

                    }

                }
                oldPhase = currentPhase;


                // Delay
                await Task.Run(() =>
                {
                    Thread.Sleep(2000);
                });



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

        private class LCUPlayerInfo
        {
            public string? PuaccountId { get; set; }
            public string? DisplayName { get; set; }
            public string? InternalName { get; set; }
            public string? NameChangeFlag { get; set; }
            public string? PercentCompleteForNextLevel { get; set; }
            public string? Privacy { get; set; }
            public string? ProfileIconId { get; set; }
            public string? Puuid { get; set; }
            public RerollPointsList? RerollPoints { get; set; }
            public string? SummonerId { get; set; }
            public string? SummonerLevel { get; set; }
            public string? Unnamed { get; set; }
            public string? XpSinceLastLevel { get; set; }
            public string? XpUntilNextLevel { get; set; }
        }

        private class RerollPointsList
        {
            public string? currentPoints { get; set; }
            public string? maxRolls { get; set; }
            public string? numberOfRolls { get; set; }
            public string? pointsCostToRoll { get; set; }
            public string? pointsToReroll { get; set; }
        }
    }










}
